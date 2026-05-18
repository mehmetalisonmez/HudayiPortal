namespace HudayiPortal.Application.Features.Sohbet.Queries.GetAvailableOgrenciler;

public sealed record AvailableOgrenciDto(
	int KullaniciId,
	string Ad,
	string Soyad,
	string? OdaNo,
	bool IsAssigned
);
