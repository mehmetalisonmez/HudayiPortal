namespace HudayiPortal.Application.Features.Sohbet.Queries.GetAllSohbetGruplari;

public sealed record SohbetGrubuDetailDto(
	int Id,
	string GrupAdi,
	string? SorumluHocaAdi,
	string? Donem,
	int OgrenciSayisi,
	int OturumSayisi,
	DateTime? OlusturulmaTarihi
);
