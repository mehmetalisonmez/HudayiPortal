using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Commands.UpdateIzinDurumu;

public sealed record UpdateIzinDurumuCommand(
	int IzinId,
	int YeniDurum
) : IRequest<bool>;
