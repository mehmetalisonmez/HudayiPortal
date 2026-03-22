using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetSession;

public sealed record CreateSohbetSessionCommand(
	int SohbetGrupId,
	DateTime Tarih,
	string KonuBasligi
) : IRequest<int>;
