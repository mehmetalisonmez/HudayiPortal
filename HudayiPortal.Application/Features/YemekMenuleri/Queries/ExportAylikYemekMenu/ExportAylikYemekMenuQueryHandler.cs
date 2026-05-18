using ClosedXML.Excel;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.ExportAylikYemekMenu;

public sealed class ExportAylikYemekMenuQueryHandler
	: IRequestHandler<ExportAylikYemekMenuQuery, byte[]>
{
	private readonly IUnitOfWork _unitOfWork;

	public ExportAylikYemekMenuQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<byte[]> Handle(
		ExportAylikYemekMenuQuery request,
		CancellationToken cancellationToken)
	{
		// ── 1) Veritabanından çek ──
		var baslangic = new DateOnly(request.Yil, request.Ay, 1);
		var bitis = baslangic.AddMonths(1).AddDays(-1);

		var menuler = await _unitOfWork.Repository<YemekMenusu>()
			.Where(m => m.SilindiMi != true
						&& m.Tarih >= baslangic
						&& m.Tarih <= bitis)
			.Include(m => m.Corba)
			.Include(m => m.AnaYemek)
			.Include(m => m.YardimciYemek)
			.Include(m => m.Ekstra)
			.Include(m => m.KahvaltiSicak1)
			.Include(m => m.KahvaltiSicak2)
			.OrderBy(m => m.Tarih)
			.ThenBy(m => m.OgunTuruId)
			.ToListAsync(cancellationToken);

		// ── 2) Tarihe göre grupla (pivot) ──
		var grouped = menuler
			.GroupBy(m => m.Tarih)
			.OrderBy(g => g.Key)
			.ToList();

		const int totalCols = 12;

		using var workbook = new XLWorkbook();
		var sheetName = $"{request.Yil}-{request.Ay:D2} Menü";
		var worksheet = workbook.Worksheets.Add(sheetName);

		// ── 3) Başlık satırı (Row 1) ──
		var titleCell = worksheet.Cell(1, 1);
		titleCell.Value = $"{request.Yil} - {request.Ay:D2} Aylık Yemek Menüsü";
		var titleRange = worksheet.Range(1, 1, 1, totalCols);
		titleRange.Merge();
		titleCell.Style.Font.Bold = true;
		titleCell.Style.Font.FontSize = 16;
		titleCell.Style.Font.FontColor = XLColor.White;
		titleCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1B5E20");
		titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
		titleCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
		worksheet.Row(1).Height = 30;

		// ── 4) Üst grup başlıkları (Row 2) — Merged ──
		// Tarih (col 1) + Gün (col 2) → dikey merge row 2-3
		var tarihHeader = worksheet.Range(2, 1, 3, 1);
		tarihHeader.Merge();
		tarihHeader.Value = "Tarih";

		var gunHeader = worksheet.Range(2, 2, 3, 2);
		gunHeader.Merge();
		gunHeader.Value = "Gün";

		// Kahvaltı (col 3-4 merged)
		var kahvaltiHeader = worksheet.Range(2, 3, 2, 4);
		kahvaltiHeader.Merge();
		kahvaltiHeader.Value = "Kahvaltı";
		kahvaltiHeader.Style.Fill.BackgroundColor = XLColor.FromHtml("#FF8F00");
		kahvaltiHeader.Style.Font.FontColor = XLColor.White;

		// Öğle (col 5-8 merged)
		var ogleHeader = worksheet.Range(2, 5, 2, 8);
		ogleHeader.Merge();
		ogleHeader.Value = "Öğle";
		ogleHeader.Style.Fill.BackgroundColor = XLColor.FromHtml("#2E7D32");
		ogleHeader.Style.Font.FontColor = XLColor.White;

		// Akşam (col 9-12 merged)
		var aksamHeader = worksheet.Range(2, 9, 2, 12);
		aksamHeader.Merge();
		aksamHeader.Value = "Akşam";
		aksamHeader.Style.Fill.BackgroundColor = XLColor.FromHtml("#1565C0");
		aksamHeader.Style.Font.FontColor = XLColor.White;

		// Row 2 ortak stil
		var row2Range = worksheet.Range(2, 1, 2, totalCols);
		row2Range.Style.Font.Bold = true;
		row2Range.Style.Font.FontSize = 12;
		row2Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
		row2Range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
		row2Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
		row2Range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

		// Tarih ve Gün merge hücreleri için arka plan
		tarihHeader.Style.Fill.BackgroundColor = XLColor.FromHtml("#37474F");
		tarihHeader.Style.Font.FontColor = XLColor.White;
		gunHeader.Style.Fill.BackgroundColor = XLColor.FromHtml("#37474F");
		gunHeader.Style.Font.FontColor = XLColor.White;

		// ── 5) Alt başlık satırı (Row 3) ──
		string[] subHeaders =
		[
			"Tarih",            // 1  (merged ile gizli)
			"Gün",              // 2  (merged ile gizli)
			"Sıcak 1",         // 3
			"Sıcak 2",         // 4
			"Çorba",           // 5
			"Ana Yemek",       // 6
			"Yardımcı Yemek",  // 7
			"Ekstra",          // 8
			"Çorba",           // 9
			"Ana Yemek",       // 10
			"Yardımcı Yemek",  // 11
			"Ekstra"           // 12
		];

		for (int i = 0; i < subHeaders.Length; i++)
		{
			var cell = worksheet.Cell(3, i + 1);
			cell.Value = subHeaders[i];
		}

		// Row 3 alt başlık renkleri — öğün grubuna göre
		// Kahvaltı alt (col 3-4)
		var kahvaltiSubRange = worksheet.Range(3, 3, 3, 4);
		kahvaltiSubRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFB300");
		kahvaltiSubRange.Style.Font.FontColor = XLColor.White;

		// Öğle alt (col 5-8)
		var ogleSubRange = worksheet.Range(3, 5, 3, 8);
		ogleSubRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#43A047");
		ogleSubRange.Style.Font.FontColor = XLColor.White;

		// Akşam alt (col 9-12)
		var aksamSubRange = worksheet.Range(3, 9, 3, 12);
		aksamSubRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1E88E5");
		aksamSubRange.Style.Font.FontColor = XLColor.White;

		var row3Range = worksheet.Range(3, 1, 3, totalCols);
		row3Range.Style.Font.Bold = true;
		row3Range.Style.Font.FontSize = 10;
		row3Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
		row3Range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
		row3Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
		row3Range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

		// ── 6) Veri satırları (pivot: her tarih = 1 satır) ──
		int rowIndex = 4;
		foreach (var group in grouped)
		{
			var tarih = group.Key;
			var kahvalti = group.FirstOrDefault(m => m.OgunTuruId == 1);
			var ogle = group.FirstOrDefault(m => m.OgunTuruId == 2);
			var aksam = group.FirstOrDefault(m => m.OgunTuruId == 3);

			worksheet.Cell(rowIndex, 1).Value = tarih.ToString("dd.MM.yyyy");
			worksheet.Cell(rowIndex, 2).Value = GunAdi(tarih);

			// Kahvaltı (OgunTuruId = 1)
			worksheet.Cell(rowIndex, 3).Value = kahvalti?.KahvaltiSicak1?.YemekAdi ?? "-";
			worksheet.Cell(rowIndex, 4).Value = kahvalti?.KahvaltiSicak2?.YemekAdi ?? "-";

			// Öğle (OgunTuruId = 2)
			worksheet.Cell(rowIndex, 5).Value = ogle?.Corba?.YemekAdi ?? "-";
			worksheet.Cell(rowIndex, 6).Value = ogle?.AnaYemek?.YemekAdi ?? "-";
			worksheet.Cell(rowIndex, 7).Value = ogle?.YardimciYemek?.YemekAdi ?? "-";
			worksheet.Cell(rowIndex, 8).Value = ogle?.Ekstra?.YemekAdi ?? "-";

			// Akşam (OgunTuruId = 3)
			worksheet.Cell(rowIndex, 9).Value = aksam?.Corba?.YemekAdi ?? "-";
			worksheet.Cell(rowIndex, 10).Value = aksam?.AnaYemek?.YemekAdi ?? "-";
			worksheet.Cell(rowIndex, 11).Value = aksam?.YardimciYemek?.YemekAdi ?? "-";
			worksheet.Cell(rowIndex, 12).Value = aksam?.Ekstra?.YemekAdi ?? "-";

			// Satır stili
			var rowRange = worksheet.Range(rowIndex, 1, rowIndex, totalCols);
			rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
			rowRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
			rowRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			rowRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

			// Zebra renklendirme
			if ((rowIndex - 4) % 2 == 0)
				rowRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E8F5E9");
			else
				rowRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFFFF");

			// Tarih ve Gün hücreleri koyu
			var tarihGunRange = worksheet.Range(rowIndex, 1, rowIndex, 2);
			tarihGunRange.Style.Font.Bold = true;

			// Hafta sonu vurgusu
			if (tarih.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
			{
				tarihGunRange.Style.Font.FontColor = XLColor.FromHtml("#D32F2F");
			}

			rowIndex++;
		}

		// ── 7) Sütun genişliklerini ayarla ──
		worksheet.Column(1).Width = 14;  // Tarih
		worksheet.Column(2).Width = 13;  // Gün
		for (int c = 3; c <= totalCols; c++)
			worksheet.Column(c).Width = 18;

		worksheet.Columns().AdjustToContents(4, rowIndex - 1);
		// Minimum genişlik garantisi
		for (int c = 1; c <= totalCols; c++)
		{
			if (worksheet.Column(c).Width < 14)
				worksheet.Column(c).Width = 14;
		}

		// ── 8) Yazdırma ayarları ──
		worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
		worksheet.PageSetup.FitToPages(1, 0);

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}

	private static string GunAdi(DateOnly tarih) => tarih.DayOfWeek switch
	{
		DayOfWeek.Monday => "Pazartesi",
		DayOfWeek.Tuesday => "Salı",
		DayOfWeek.Wednesday => "Çarşamba",
		DayOfWeek.Thursday => "Perşembe",
		DayOfWeek.Friday => "Cuma",
		DayOfWeek.Saturday => "Cumartesi",
		DayOfWeek.Sunday => "Pazar",
		_ => ""
	};
}
