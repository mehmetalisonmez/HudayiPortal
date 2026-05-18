namespace HudayiPortal.Application.Features.YemekTanimlari.Queries.GetAllYemekTanimlari;

public sealed record YemekTanimiDto(
	int Id,
	string YemekAdi,
	int KategoriId,
	string KategoriAdi,
	int? Kalori,
	string? ResimUrl
);
