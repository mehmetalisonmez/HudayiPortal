namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikKatilimcilari;

public sealed record KatilimciDto(
	int Id,
	int KullaniciId,
	string Ad,
	string Soyad,
	bool? KatilimDurumu,
	DateTime? OlusturulmaTarihi
);
