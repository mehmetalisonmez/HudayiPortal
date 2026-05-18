using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Queries.GetAllYemekTanimlari;

public sealed record GetAllYemekTanimlariQuery : IRequest<List<YemekTanimiDto>>;
