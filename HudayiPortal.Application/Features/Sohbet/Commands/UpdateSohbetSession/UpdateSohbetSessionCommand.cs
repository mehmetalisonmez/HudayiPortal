using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.UpdateSohbetSession;

public sealed record UpdateSohbetSessionCommand(
	int Id,
	DateTime Tarih,
	string KonuBasligi
) : IRequest<Unit>;
