namespace HudayiPortal.Application.Features.Kullanicilar.Queries.GetOgrenciList;

public sealed record KullaniciListDto(
	int Id,
	string Ad,
	string Soyad,
	string? TcKimlikNo,
	string? Telefon,
	string? Email,
	string? OdaNo,
	int? Kat,
	bool? AktifMi
);