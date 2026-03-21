using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Queries.GetIzinTalepleri;

public sealed record GetIzinTalepleriQuery(
	int? KullaniciId,
	int? OnayDurumu
) : IRequest<List<IzinDto>>;
