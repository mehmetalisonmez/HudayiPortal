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

		using var workbook = new XLWorkbook();
		var sheetName = $"{request.Yil}-{request.Ay:D2} Menü";
		var worksheet = workbook.Worksheets.Add(sheetName);

		// ── Title row ──
		var titleCell = worksheet.Cell(1, 1);
		titleCell.Value = $"{request.Yil} - {request.Ay:D2} Aylık Yemek Menüsü";
		worksheet.Range(1, 1, 1, 8).Merge();
		titleCell.Style.Font.Bold = true;
		titleCell.Style.Font.FontSize = 14;
		titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

		// ── Header row ──
		string[] headers = ["Tarih", "Öğün", "Çorba", "Ana Yemek", "Yardımcı Yemek", "Ekstra", "Kahvaltı Sıcak 1", "Kahvaltı Sıcak 2"];
		for (int i = 0; i < headers.Length; i++)
		{
			var cell = worksheet.Cell(3, i + 1);
			cell.Value = headers[i];
		}

		var headerRange = worksheet.Range(3, 1, 3, headers.Length);
		headerRange.Style.Font.Bold = true;
		headerRange.Style.Font.FontColor = XLColor.White;
		headerRange.Style.Fill.BackgroundColor = XLColor.DarkGreen;
		headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
		headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
		headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

		// ── Data rows ──
		for (int i = 0; i < menuler.Count; i++)
		{
			var m = menuler[i];
			int row = i + 4;

			worksheet.Cell(row, 1).Value = m.Tarih.ToString("dd.MM.yyyy");
			worksheet.Cell(row, 2).Value = OgunTuruAdi(m.OgunTuruId);
			worksheet.Cell(row, 3).Value = m.Corba?.YemekAdi ?? "-";
			worksheet.Cell(row, 4).Value = m.AnaYemek?.YemekAdi ?? "-";
			worksheet.Cell(row, 5).Value = m.YardimciYemek?.YemekAdi ?? "-";
			worksheet.Cell(row, 6).Value = m.Ekstra?.YemekAdi ?? "-";
			worksheet.Cell(row, 7).Value = m.KahvaltiSicak1?.YemekAdi ?? "-";
			worksheet.Cell(row, 8).Value = m.KahvaltiSicak2?.YemekAdi ?? "-";

			// Alternate row coloring
			var rowRange = worksheet.Range(row, 1, row, headers.Length);
			rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
			rowRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
			if (i % 2 == 0)
			{
				rowRange.Style.Fill.BackgroundColor = XLColor.LightGreen;
			}
		}

		worksheet.Columns().AdjustToContents();

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}

	private static string OgunTuruAdi(int ogunTuruId) => ogunTuruId switch
	{
		1 => "Kahvaltı",
		2 => "Öğle Yemeği",
		3 => "Akşam Yemeği",
		_ => $"Öğün {ogunTuruId}"
	};
}
