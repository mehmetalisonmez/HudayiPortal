using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;

public sealed record GetSohbetGrubuByIdQuery(int Id) : IRequest<SohbetGrubuFullDto>;
