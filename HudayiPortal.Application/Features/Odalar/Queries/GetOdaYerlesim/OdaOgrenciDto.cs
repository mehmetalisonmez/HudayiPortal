namespace HudayiPortal.Application.Features.Odalar.Queries.GetOdaYerlesim;

public sealed record OdaOgrenciDto(
	int KullaniciId,
	string Ad,
	string Soyad,
	string? Telefon
);
