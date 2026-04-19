using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Commands.DeleteIzinTalebi;

public sealed record DeleteIzinTalebiCommand(int Id) : IRequest<Unit>;
