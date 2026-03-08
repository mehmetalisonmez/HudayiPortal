using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateYemekMenu;

public sealed record CreateYemekMenuCommand(
	DateOnly Tarih,
	int OgunTuruId,
	int? CorbaId,
	int? AnaYemekId,
	int? YardimciYemekId,
	int? EkstraId,
	int? KahvaltiSicak1Id,
	int? KahvaltiSicak2Id
) : IRequest<int>;
