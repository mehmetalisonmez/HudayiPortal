namespace HudayiPortal.Application.Features.Odalar.Queries.GetOdaYerlesim;

public sealed record OdasizOgrenciDto(
	int KullaniciId,
	string Ad,
	string Soyad,
	string? Telefon
);
