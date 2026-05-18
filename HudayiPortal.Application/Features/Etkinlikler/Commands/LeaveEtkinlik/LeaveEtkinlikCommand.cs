using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.LeaveEtkinlik;

public sealed record LeaveEtkinlikCommand(int EtkinlikId) : IRequest<Unit>;
