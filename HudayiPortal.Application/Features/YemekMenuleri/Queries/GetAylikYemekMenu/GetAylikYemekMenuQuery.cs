using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetAylikYemekMenu;

public sealed record GetAylikYemekMenuQuery(
	int Yil,
	int Ay
) : IRequest<List<YemekMenuDto>>;
