namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikler;

public sealed record EtkinlikListDto(
	int Id,
	string Baslik,
	string? Aciklama,
	DateTime BaslangicTarihi,
	DateTime? BitisTarihi,
	DateTime? SonKayitTarihi,
	decimal? Ucret,
	bool? ZorunluMu,
	string? ResimUrl,
	int BegeniSayisi,
	int YorumSayisi,
	int KatilimciSayisi,
	bool IsLiked,
	bool IsJoined
);
