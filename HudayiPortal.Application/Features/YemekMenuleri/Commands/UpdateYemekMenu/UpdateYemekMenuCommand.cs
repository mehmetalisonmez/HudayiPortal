using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.UpdateYemekMenu;

public sealed record UpdateYemekMenuCommand(
	int Id,
	DateOnly Tarih,
	int OgunTuruId,
	int? CorbaId,
	int? AnaYemekId,
	int? YardimciYemekId,
	int? EkstraId,
	int? KahvaltiSicak1Id,
	int? KahvaltiSicak2Id
) : IRequest<Unit>;
