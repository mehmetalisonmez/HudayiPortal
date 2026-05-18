using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;
using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetMyNobetler;

public sealed record GetMyNobetlerQuery(
	DateOnly? StartDate,
	DateOnly? EndDate
) : IRequest<List<PersonelNobetDto>>;
