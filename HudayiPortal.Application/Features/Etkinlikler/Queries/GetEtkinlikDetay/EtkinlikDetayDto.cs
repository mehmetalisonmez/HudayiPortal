namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikDetay;

public sealed record EtkinlikDetayDto(
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
	bool IsJoined,
	List<YorumDto> Yorumlar
);
