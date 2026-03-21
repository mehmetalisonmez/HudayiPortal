using MediatR;

namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;

public sealed record GetSikayetlerQuery(
	int? GonderenKullaniciId,
	int? Durum
) : IRequest<List<SikayetDto>>;
