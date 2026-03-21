using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetAktifEtkinlikler;

public sealed record GetAktifEtkinliklerQuery : IRequest<List<EtkinlikDto>>;
