namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetAktifEtkinlikler;

public sealed record EtkinlikDto(
	int Id,
	string Baslik,
	string? Aciklama,
	DateTime BaslangicTarihi,
	DateTime? BitisTarihi,
	DateTime? SonKayitTarihi,
	decimal? Ucret,
	bool? ZorunluMu,
	string? ResimUrl
);
