namespace HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajlar;

public sealed record MesajDto(
	int Id,
	int GonderenId,
	string GonderenAdSoyad,
	int? AliciId,
	int? ChatGrupId,
	string MesajIcerigi,
	DateTime? OlusturulmaTarihi
);
