using MediatR;

namespace HudayiPortal.Application.Features.IslemKategorileri.Commands.UpdateIslemKategorisi;

public sealed record UpdateIslemKategorisiCommand(
	int Id,
	string KategoriAdi,
	int YonId
) : IRequest;
