using ClosedXML.Excel;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.ExportAylikYoklama;

public sealed class ExportAylikYoklamaQueryHandler
	: IRequestHandler<ExportAylikYoklamaQuery, byte[]>
{
	private readonly IUnitOfWork _unitOfWork;

	public ExportAylikYoklamaQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<byte[]> Handle(
		ExportAylikYoklamaQuery request,
		CancellationToken cancellationToken)
	{
		var baslangic = new DateOnly(request.Yil, request.Ay, 1);
		var bitis = baslangic.AddMonths(1).AddDays(-1);
		var gunSayisi = DateTime.DaysInMonth(request.Yil, request.Ay);

		// Fetch attendance records for the given month, year, and type
		var yoklamalar = await _unitOfWork.Repository<GunlukYoklama>()
			.Where(y => y.YoklamaTurId == request.YoklamaTurId
						&& y.Tarih >= baslangic
						&& y.Tarih <= bitis
						&& y.SilindiMi != true)
			.Include(y => y.Kullanici)
				.ThenInclude(k => k.Oda)
			.ToListAsync(cancellationToken);

		// Group by student
		var ogrenciler = yoklamalar
			.GroupBy(y => y.KullaniciId)
			.Select(g => new
			{
				KullaniciId = g.Key,
				Ad = g.First().Kullanici.Ad,
				Soyad = g.First().Kullanici.Soyad,
				OdaNo = g.First().Kullanici.Oda?.OdaNo ?? "-",
				Gunler = g.ToDictionary(y => y.Tarih.Day, y => y.Durum)
			})
			.OrderBy(o => o.OdaNo)
			.ThenBy(o => o.Soyad)
			.ThenBy(o => o.Ad)
			.ToList();

		using var workbook = new XLWorkbook();
		var worksheet = workbook.Worksheets.Add($"{request.Yil}-{request.Ay:D2}");

		// Header row
		worksheet.Cell(1, 1).Value = "Oda No";
		worksheet.Cell(1, 2).Value = "Ad";
		worksheet.Cell(1, 3).Value = "Soyad";
		for (int gun = 1; gun <= gunSayisi; gun++)
		{
			worksheet.Cell(1, 3 + gun).Value = gun;
		}

		// Style header
		var headerRange = worksheet.Range(1, 1, 1, 3 + gunSayisi);
		headerRange.Style.Font.Bold = true;
		headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

		// Data rows
		for (int i = 0; i < ogrenciler.Count; i++)
		{
			var ogrenci = ogrenciler[i];
			int row = i + 2;
			worksheet.Cell(row, 1).Value = ogrenci.OdaNo;
			worksheet.Cell(row, 2).Value = ogrenci.Ad;
			worksheet.Cell(row, 3).Value = ogrenci.Soyad;

			for (int gun = 1; gun <= gunSayisi; gun++)
			{
				if (ogrenci.Gunler.TryGetValue(gun, out var durum))
				{
					worksheet.Cell(row, 3 + gun).Value = durum ? "Var" : "Yok";
				}
			}
		}

		worksheet.Columns().AdjustToContents();

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}
}
