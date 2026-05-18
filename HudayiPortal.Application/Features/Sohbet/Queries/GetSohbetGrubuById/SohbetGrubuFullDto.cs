namespace HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;

public sealed record SohbetGrubuFullDto(
	int Id,
	string GrupAdi,
	string? SorumluHocaAdi,
	string? Donem,
	DateTime? OlusturulmaTarihi,
	List<GrupOgrenciDto> Ogrenciler,
	List<GrupOturumDto> Oturumlar
);
