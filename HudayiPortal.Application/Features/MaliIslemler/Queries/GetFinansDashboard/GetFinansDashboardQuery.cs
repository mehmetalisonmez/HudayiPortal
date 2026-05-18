using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Queries.GetFinansDashboard;

public sealed record GetFinansDashboardQuery(
	DateTime? BaslangicTarihi,
	DateTime? BitisTarihi
) : IRequest<FinansDashboardDto>;
