using HudayiPortal.Application.Features.Duyurular.Queries.GetAktifDuyurular;
using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Queries.GetAllDuyurular;

/// <summary>
/// Admin/Personel için tüm duyuruları (süresi dolmuş dahil) getirir.
/// Sadece soft-delete edilmemiş kayıtlar döner.
/// </summary>
public sealed record GetAllDuyurularQuery : IRequest<List<DuyuruDto>>;
