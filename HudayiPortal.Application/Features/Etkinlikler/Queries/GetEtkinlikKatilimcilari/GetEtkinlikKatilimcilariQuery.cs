using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikKatilimcilari;

public sealed record GetEtkinlikKatilimcilariQuery(int EtkinlikId) : IRequest<List<KatilimciDto>>;
