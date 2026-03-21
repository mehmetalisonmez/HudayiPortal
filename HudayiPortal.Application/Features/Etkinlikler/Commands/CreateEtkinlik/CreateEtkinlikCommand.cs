using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.CreateEtkinlik;

public sealed record CreateEtkinlikCommand(
	string Baslik,
	string Aciklama,
	DateTime BaslangicTarihi,
	DateTime BitisTarihi,
	DateTime? SonKayitTarihi,
	decimal? Ucret,
	bool ZorunluMu,
	string? ResimUrl
) : IRequest<int>;
