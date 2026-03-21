using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Queries.GetAktifDuyurular;

public sealed record GetAktifDuyurularQuery : IRequest<List<DuyuruDto>>;
