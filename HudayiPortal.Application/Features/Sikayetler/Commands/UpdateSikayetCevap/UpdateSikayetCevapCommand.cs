using MediatR;

namespace HudayiPortal.Application.Features.Sikayetler.Commands.UpdateSikayetCevap;

public sealed record UpdateSikayetCevapCommand(
	int SikayetId,
	string Cevap,
	int YeniDurum
) : IRequest<bool>;
