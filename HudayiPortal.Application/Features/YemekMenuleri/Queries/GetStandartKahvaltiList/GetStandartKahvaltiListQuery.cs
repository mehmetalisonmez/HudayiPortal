using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetStandartKahvaltiList;

public sealed record GetStandartKahvaltiListQuery() : IRequest<List<StandartKahvaltiDto>>;
