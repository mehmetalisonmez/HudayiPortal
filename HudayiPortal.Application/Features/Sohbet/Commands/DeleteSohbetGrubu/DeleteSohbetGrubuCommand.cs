using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.DeleteSohbetGrubu;

public sealed record DeleteSohbetGrubuCommand(int Id) : IRequest<Unit>;
