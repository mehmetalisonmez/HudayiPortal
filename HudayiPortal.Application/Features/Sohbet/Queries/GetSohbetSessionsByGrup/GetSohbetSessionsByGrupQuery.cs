using MediatR;
using HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetSessionsByGrup;

public sealed record GetSohbetSessionsByGrupQuery(int GrupId) : IRequest<List<GrupOturumDto>>;
