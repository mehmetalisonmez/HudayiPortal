using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;

public sealed record GetPersonelNobetleriQuery(
	int? PersonelId,
	DateTime? BaslangicTarihi,
	DateTime? BitisTarihi
) : IRequest<List<PersonelNobetDto>>;
