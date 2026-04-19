using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Queries.GetIzinTurleri;

public sealed record GetIzinTurleriQuery() : IRequest<List<IzinTuruDto>>;
