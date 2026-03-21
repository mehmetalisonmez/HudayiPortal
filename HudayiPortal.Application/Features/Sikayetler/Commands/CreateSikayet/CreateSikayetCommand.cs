using MediatR;

namespace HudayiPortal.Application.Features.Sikayetler.Commands.CreateSikayet;

public sealed record CreateSikayetCommand(
	string Baslik,
	string Icerik
) : IRequest<int>;
