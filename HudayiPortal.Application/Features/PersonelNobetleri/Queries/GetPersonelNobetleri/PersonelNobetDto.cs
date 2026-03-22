namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;

public sealed record PersonelNobetDto(
	int Id,
	int PersonelId,
	string PersonelAdSoyad,
	DateTime Tarih,
	string NobetTuru,
	string? Aciklama
);
