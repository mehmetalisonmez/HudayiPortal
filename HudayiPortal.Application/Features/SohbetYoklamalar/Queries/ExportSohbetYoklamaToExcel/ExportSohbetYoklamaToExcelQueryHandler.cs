using ClosedXML.Excel;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.SohbetYoklamalar.Queries.ExportSohbetYoklamaToExcel;

public sealed class ExportSohbetYoklamaToExcelQueryHandler
	: IRequestHandler<ExportSohbetYoklamaToExcelQuery, byte[]>
{
	private readonly IUnitOfWork _unitOfWork;

	public ExportSohbetYoklamaToExcelQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<byte[]> Handle(
		ExportSohbetYoklamaToExcelQuery request,
		CancellationToken cancellationToken)
	{
		var startDt = request.StartDate.ToDateTime(TimeOnly.MinValue);
		var endDt = request.EndDate.ToDateTime(TimeOnly.MaxValue);

		// 1 — Tarih aralığındaki sohbet oturumlarını bul
		var sohbetler = await _unitOfWork.Repository<SohbetEntity>()
			.Where(s => s.SohbetGrupId == request.GrupId
						&& s.Tarih >= startDt && s.Tarih <= endDt
						&& s.SilindiMi != true)
			.OrderBy(s => s.Tarih)
			.ToListAsync(cancellationToken);

		var sohbetIds = sohbetler.Select(s => s.Id).ToList();

		// 2 — Bu gruba kayıtlı aktif öğrenciler
		var ogrenciler = await _unitOfWork.Repository<OgrenciSohbetGrubu>()
			.Where(og => og.SohbetGrupId == request.GrupId && og.SilindiMi != true)
			.Where(og => og.Kullanici.RolId == 1
						 && og.Kullanici.AktifMi == true
						 && og.Kullanici.SilindiMi != true)
			.Select(og => new
			{
				og.Kullanici.Id,
				og.Kullanici.Ad,
				og.Kullanici.Soyad,
				OdaNo = og.Kullanici.Oda != null ? og.Kullanici.Oda.OdaNo : null
			})
			.OrderBy(o => o.OdaNo ?? "")
			.ThenBy(o => o.Soyad)
			.ThenBy(o => o.Ad)
			.ToListAsync(cancellationToken);

		// 3 — Tüm yoklama kayıtları
		var yoklamalar = await _unitOfWork.Repository<SohbetYoklama>()
			.Where(sy => sohbetIds.Contains(sy.SohbetId) && sy.SilindiMi != true)
			.ToListAsync(cancellationToken);

		// (KullaniciId, SohbetId) → yoklama kaydı
		var yoklamaMap = yoklamalar
			.ToDictionary(y => (y.KullaniciId, y.SohbetId));

		// 4 — Excel oluştur
		using var workbook = new XLWorkbook();
		var ws = workbook.Worksheets.Add("Sohbet Yoklama");

		// Başlık satırı
		ws.Cell(1, 1).Value = "Ad Soyad";
		ws.Cell(1, 2).Value = "Oda No";
		for (int i = 0; i < sohbetler.Count; i++)
			ws.Cell(1, 3 + i).Value = DateOnly.FromDateTime(sohbetler[i].Tarih).ToString("dd.MM");

		var headerRange = ws.Range(1, 1, 1, 2 + Math.Max(sohbetler.Count, 1));
		headerRange.Style.Font.Bold = true;
		headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
		headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

		// Veri satırları
		for (int r = 0; r < ogrenciler.Count; r++)
		{
			var ogr = ogrenciler[r];
			int row = r + 2;
			ws.Cell(row, 1).Value = $"{ogr.Ad} {ogr.Soyad}";
			ws.Cell(row, 2).Value = ogr.OdaNo ?? "-";

			for (int c = 0; c < sohbetler.Count; c++)
			{
				var key = (ogr.Id, sohbetler[c].Id);
				string hucre;
				if (yoklamaMap.TryGetValue(key, out var kayit))
				{
					if (kayit.KatilimDurumu)
						hucre = "Var";
					else if (!string.IsNullOrWhiteSpace(kayit.MazeretAciklamasi))
						hucre = "Mazeret";
					else
						hucre = "Yok";
				}
				else
				{
					hucre = "-";
				}

				var cell = ws.Cell(row, 3 + c);
				cell.Value = hucre;
				cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				if (hucre == "Var")
					cell.Style.Font.FontColor = XLColor.Green;
				else if (hucre == "Yok")
					cell.Style.Font.FontColor = XLColor.Red;
				else if (hucre == "Mazeret")
					cell.Style.Font.FontColor = XLColor.Orange;
			}
		}

		ws.Columns().AdjustToContents();

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}
}
