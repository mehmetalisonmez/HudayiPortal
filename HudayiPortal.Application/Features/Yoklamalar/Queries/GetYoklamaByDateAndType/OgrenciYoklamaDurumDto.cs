namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetYoklamaByDateAndType;

public sealed record OgrenciYoklamaDurumDto(
	int KullaniciId,
	string Ad,
	string Soyad,
	string? OdaNo,
	bool? Durum,
	string? Aciklama
);
