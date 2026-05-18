using MediatR;

namespace HudayiPortal.Application.Features.IslemKategorileri.Queries.GetIslemKategorileri;

public sealed record GetIslemKategorileriQuery(
	int? YonId
) : IRequest<List<IslemKategorisiDto>>;
