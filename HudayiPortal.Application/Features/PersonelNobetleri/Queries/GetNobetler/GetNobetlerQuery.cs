using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;
using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetNobetler;

public sealed record GetNobetlerQuery(
	DateOnly StartDate,
	DateOnly EndDate
) : IRequest<List<PersonelNobetDto>>;
