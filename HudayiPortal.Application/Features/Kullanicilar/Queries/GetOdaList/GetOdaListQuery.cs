using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Queries.GetOdaList;

public sealed record GetOdaListQuery() : IRequest<List<OdaListDto>>;
