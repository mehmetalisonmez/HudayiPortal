using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Queries.GetDuyurular;

public sealed record GetDuyurularQuery : IRequest<List<DuyuruListDto>>;
