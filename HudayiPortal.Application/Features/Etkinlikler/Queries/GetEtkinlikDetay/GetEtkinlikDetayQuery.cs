using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikDetay;

public sealed record GetEtkinlikDetayQuery(int Id) : IRequest<EtkinlikDetayDto>;
