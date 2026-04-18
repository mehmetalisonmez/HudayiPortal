using MediatR;

namespace HudayiPortal.Application.Features.Dashboard.Queries.GetOgrenciDashboard;

public sealed record GetOgrenciDashboardQuery : IRequest<OgrenciDashboardDto>;