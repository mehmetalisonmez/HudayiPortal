using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.UpdateKatilimDurumu;

/// <summary>
/// Admin/Personel tarafından katılımcının katılım durumunu günceller.
/// KatilimDurumu: null = Bekleniyor, true = Katıldı, false = Katılmadı
/// </summary>
public sealed record UpdateKatilimDurumuCommand(int KatilimciId, bool? KatilimDurumu) : IRequest<Unit>;
