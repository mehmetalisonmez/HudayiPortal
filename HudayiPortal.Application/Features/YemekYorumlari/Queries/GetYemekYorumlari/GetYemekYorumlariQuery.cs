using MediatR;

namespace HudayiPortal.Application.Features.YemekYorumlari.Queries.GetYemekYorumlari;

public sealed record GetYemekYorumlariQuery(int YemekMenuId) : IRequest<List<YemekYorumDto>>;
