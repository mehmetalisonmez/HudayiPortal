using MediatR;

namespace HudayiPortal.Application.Features.Mesajlar.Commands.CreateMesaj;

public sealed record CreateMesajCommand(
	int? AliciId,
	int? ChatGrupId,
	string MesajIcerigi
) : IRequest<int>;
