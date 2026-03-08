using MediatR;

namespace HudayiPortal.Application.Features.Mesajlar.Commands.SendMessage;

public sealed record SendMessageCommand(
	int? AliciId,
	int? ChatGrupId,
	string MesajIcerigi
) : IRequest<int>;
