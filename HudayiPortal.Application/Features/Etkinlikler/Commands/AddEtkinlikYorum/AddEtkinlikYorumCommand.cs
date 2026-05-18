using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.AddEtkinlikYorum;

public sealed record AddEtkinlikYorumCommand(int EtkinlikId, string YorumMetni) : IRequest<int>;
