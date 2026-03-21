using MediatR;

namespace HudayiPortal.Application.Features.YemekYorumlari.Commands.CreateYemekYorum;

public sealed record CreateYemekYorumCommand(
	int YemekMenuId,
	string YorumMetni,
	int? Puan
) : IRequest<int>;
