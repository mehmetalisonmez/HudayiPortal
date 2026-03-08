namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetAylikYemekMenu;

public sealed record YemekMenuDto(
	int Id,
	DateOnly Tarih,
	int OgunTuruId,
	string? CorbaAdi,
	string? AnaYemekAdi,
	string? YardimciYemekAdi,
	string? EkstraAdi,
	string? KahvaltiSicak1Adi,
	string? KahvaltiSicak2Adi
);
