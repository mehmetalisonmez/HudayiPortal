using MediatR;

namespace HudayiPortal.Application.Features.Roller.Queries.GetRolList;

public sealed record GetRolListQuery : IRequest<List<RolDto>>;
