using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.ToggleLike;

/// <summary>
/// Etkinliği beğen veya beğeniyi geri al.
/// Handler true (beğenildi) veya false (beğeni kaldırıldı) döner.
/// </summary>
public sealed record ToggleLikeCommand(int EtkinlikId) : IRequest<bool>;
