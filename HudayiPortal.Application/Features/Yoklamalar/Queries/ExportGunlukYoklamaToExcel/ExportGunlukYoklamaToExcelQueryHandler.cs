using ClosedXML.Excel;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.ExportGunlukYoklamaToExcel;

public sealed class ExportGunlukYoklamaToExcelQueryHandler
	: IRequestHandler<ExportGunlukYoklamaToExcelQuery, byte[]>
{
	private readonly IUnitOfWork _unitOfWork;

	public ExportGunlukYoklamaToExcelQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<byte[]> Handle(
		ExportGunlukYoklamaToExcelQuery request,
		CancellationToken cancellationToken)
	{
		// 1 — Tarih aralığındaki tüm günleri hesapla
		var gunler = new List<DateOnly>();
		for (var d = request.StartDate; d <= request.EndDate; d = d.AddDays(1))
			gunler.Add(d);

		// 2 — Yoklama kayıtlarını çek
		var yoklamalar = await _unitOfWork.Repository<GunlukYoklama>()
			.Where(y => y.YoklamaTurId == request.YoklamaTurId
						&& y.Tarih >= request.StartDate
						&& y.Tarih <= request.EndDate
						&& y.SilindiMi != true)
			.Include(y => y.Kullanici)
				.ThenInclude(k => k.Oda)
			.ToListAsync(cancellationToken);

		// 3 — Tüm aktif öğrencileri çek (yoklama almamış olanlar da satırda görünsün)
		var tumOgrenciler = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.RolId == 1 && k.AktifMi == true && k.SilindiMi != true)
			.Include(k => k.Oda)
			.OrderBy(k => k.Oda != null ? k.Oda.OdaNo : "")
			.ThenBy(k => k.Soyad)
			.ThenBy(k => k.Ad)
			.ToListAsync(cancellationToken);

		// 4 — Yoklamaları (KullaniciId, Tarih) → kayıt şeklinde indeksle
		var yoklamaMap = yoklamalar
			.ToDictionary(y => (y.KullaniciId, y.Tarih));

		// 5 — Excel oluştur
		using var workbook = new XLWorkbook();
		var ws = workbook.Worksheets.Add("Günlük Yoklama");

		// Başlık satırı
		ws.Cell(1, 1).Value = "Ad Soyad";
		ws.Cell(1, 2).Value = "Oda No";
		for (int i = 0; i < gunler.Count; i++)
			ws.Cell(1, 3 + i).Value = gunler[i].ToString("dd.MM");

		var headerRange = ws.Range(1, 1, 1, 2 + gunler.Count);
		headerRange.Style.Font.Bold = true;
		headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
		headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

		// Veri satırları
		for (int r = 0; r < tumOgrenciler.Count; r++)
		{
			var ogr = tumOgrenciler[r];
			int row = r + 2;
			ws.Cell(row, 1).Value = $"{ogr.Ad} {ogr.Soyad}";
			ws.Cell(row, 2).Value = ogr.Oda?.OdaNo ?? "-";

			for (int c = 0; c < gunler.Count; c++)
			{
				var key = (ogr.Id, gunler[c]);
				string hucre;
				if (yoklamaMap.TryGetValue(key, out var kayit))
				{
					if (kayit.Durum)
						hucre = "Var";
					else if (!string.IsNullOrWhiteSpace(kayit.Aciklama))
						hucre = "İzinli";
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

				// Renk kodlama
				if (hucre == "Var")
					cell.Style.Font.FontColor = XLColor.Green;
				else if (hucre == "Yok")
					cell.Style.Font.FontColor = XLColor.Red;
				else if (hucre == "İzinli")
					cell.Style.Font.FontColor = XLColor.Orange;
			}
		}

		ws.Columns().AdjustToContents();

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}
}
