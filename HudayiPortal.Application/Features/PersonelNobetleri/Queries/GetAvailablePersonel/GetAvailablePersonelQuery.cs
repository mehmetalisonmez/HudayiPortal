using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetAvailablePersonel;

public sealed record GetAvailablePersonelQuery : IRequest<List<AvailablePersonelDto>>;
