using MediatR;

namespace HudayiPortal.Application.Features.IslemKategorileri.Commands.CreateIslemKategorisi;

public sealed record CreateIslemKategorisiCommand(
	string KategoriAdi,
	int YonId
) : IRequest<int>;
