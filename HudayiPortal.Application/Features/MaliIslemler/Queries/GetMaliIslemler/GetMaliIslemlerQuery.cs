using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Queries.GetMaliIslemler;

public sealed record GetMaliIslemlerQuery(
	int? YonId,
	DateTime? BaslangicTarihi,
	DateTime? BitisTarihi,
	int? KategoriId
) : IRequest<List<MaliIslemDto>>;
