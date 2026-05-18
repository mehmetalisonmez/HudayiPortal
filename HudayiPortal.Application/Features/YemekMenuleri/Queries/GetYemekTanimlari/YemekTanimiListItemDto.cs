namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetYemekTanimlari;

public sealed record YemekTanimiListItemDto(
	int Id,
	string YemekAdi,
	int KategoriId
);
