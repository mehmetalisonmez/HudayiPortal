namespace HudayiPortal.Application.Features.SohbetYoklamalar.Queries.GetSohbetYoklama;

public sealed record SohbetOgrenciDurumDto(
	int KullaniciId,
	string Ad,
	string Soyad,
	string? OdaNo,
	bool? KatilimDurumu,
	string? MazeretAciklamasi
);
