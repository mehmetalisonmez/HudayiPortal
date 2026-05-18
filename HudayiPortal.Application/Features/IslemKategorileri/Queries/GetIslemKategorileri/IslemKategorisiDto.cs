namespace HudayiPortal.Application.Features.IslemKategorileri.Queries.GetIslemKategorileri;

public sealed record IslemKategorisiDto(
	int Id,
	string KategoriAdi,
	int YonId,
	string YonAdi
);
