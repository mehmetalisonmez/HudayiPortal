namespace HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajGecmisi;

public sealed record MesajDto(
	int Id,
	int GonderenId,
	int? AliciId,
	int? ChatGrupId,
	string MesajIcerigi,
	bool? OkunduMu,
	DateTime? OlusturulmaTarihi
);
