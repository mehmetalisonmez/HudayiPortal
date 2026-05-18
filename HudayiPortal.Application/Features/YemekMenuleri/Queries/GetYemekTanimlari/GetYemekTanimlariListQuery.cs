using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetYemekTanimlari;

public sealed record GetYemekTanimlariListQuery() : IRequest<List<YemekTanimiListItemDto>>;
