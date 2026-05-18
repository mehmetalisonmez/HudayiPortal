namespace HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;

public sealed record GrupOgrenciDto(
	int KullaniciId,
	string Ad,
	string Soyad,
	string? OdaNo,
	DateTime? AtanmaTarihi
);
