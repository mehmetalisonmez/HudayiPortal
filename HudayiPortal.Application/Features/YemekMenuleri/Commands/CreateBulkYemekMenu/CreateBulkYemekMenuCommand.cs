using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateBulkYemekMenu;

public sealed record BulkYemekMenuItemDto(
	DateOnly Tarih,
	int OgunTuruId,
	int? CorbaId,
	int? AnaYemekId,
	int? YardimciYemekId,
	int? EkstraId,
	int? KahvaltiSicak1Id,
	int? KahvaltiSicak2Id
);

public sealed record CreateBulkYemekMenuCommand(
	List<BulkYemekMenuItemDto> Menuler
) : IRequest<int>;
