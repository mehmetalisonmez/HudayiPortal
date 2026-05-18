using MediatR;

namespace HudayiPortal.Application.Features.Odalar.Queries.GetOdaYerlesim;

public sealed record GetOdaYerlesimQuery() : IRequest<OdaYerlesimResultDto>;
