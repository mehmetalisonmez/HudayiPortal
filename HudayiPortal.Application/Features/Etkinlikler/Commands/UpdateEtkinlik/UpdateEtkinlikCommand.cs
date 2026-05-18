using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.UpdateEtkinlik;

public sealed record UpdateEtkinlikCommand(
	int Id,
	string Baslik,
	string? Aciklama,
	DateTime BaslangicTarihi,
	DateTime? BitisTarihi,
	DateTime? SonKayitTarihi,
	decimal? Ucret,
	bool ZorunluMu,
	string? ResimUrl
) : IRequest<Unit>;
