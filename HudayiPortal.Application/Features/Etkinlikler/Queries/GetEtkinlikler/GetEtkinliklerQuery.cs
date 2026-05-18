using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikler;

/// <summary>
/// Etkinlikleri filtreli olarak listeler.
/// Aktif: null=tümü, true=aktif (bitisTarihi>=şimdi), false=geçmiş
/// Ucretsiz: null=tümü, true=yalnızca ücretsiz, false=yalnızca ücretli
/// </summary>
public sealed record GetEtkinliklerQuery(bool? Aktif, bool? Ucretsiz) : IRequest<List<EtkinlikListDto>>;
