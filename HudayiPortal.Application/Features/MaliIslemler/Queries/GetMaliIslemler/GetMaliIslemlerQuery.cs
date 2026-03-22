using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Queries.GetMaliIslemler;

public sealed record GetMaliIslemlerQuery(
	int? YonId,
	DateTime? BaslangicTarihi,
	DateTime? BitisTarihi
) : IRequest<List<MaliIslemDto>>;
