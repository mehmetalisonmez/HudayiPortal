using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.DeleteEtkinlik;

public sealed record DeleteEtkinlikCommand(int Id) : IRequest<Unit>;
