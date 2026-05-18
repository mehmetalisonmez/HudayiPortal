namespace HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;

public sealed record GrupOturumDto(
	int Id,
	DateTime Tarih,
	string? KonuBasligi,
	int YoklamaSayisi
);
