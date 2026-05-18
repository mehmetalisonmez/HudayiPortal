using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.DeleteSohbetSession;

public sealed record DeleteSohbetSessionCommand(int Id) : IRequest<Unit>;
