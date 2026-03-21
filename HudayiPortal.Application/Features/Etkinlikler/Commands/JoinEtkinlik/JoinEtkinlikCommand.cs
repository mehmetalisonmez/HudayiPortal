using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.JoinEtkinlik;

public sealed record JoinEtkinlikCommand(int EtkinlikId) : IRequest<int>;
