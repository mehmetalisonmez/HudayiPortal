using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetYoklamaTurleri;

public sealed record GetYoklamaTurleriQuery : IRequest<List<YoklamaTuruDto>>;
