using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Queries.GetYemekKategorileri;

public sealed record GetYemekKategorileriListQuery : IRequest<List<YemekKategorisiDto>>;
