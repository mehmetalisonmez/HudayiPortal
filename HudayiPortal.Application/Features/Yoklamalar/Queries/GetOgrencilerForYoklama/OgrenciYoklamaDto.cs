namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetOgrencilerForYoklama;

public sealed record OgrenciYoklamaDto(
	int KullaniciId,
	string Ad,
	string Soyad,
	int? OdaId,
	string? OdaNo
);
