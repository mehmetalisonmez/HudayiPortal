using MediatR;

namespace HudayiPortal.Application.Features.Dashboard.Queries.GetYoneticiDashboard;

public sealed record GetYoneticiDashboardQuery : IRequest<YoneticiDashboardDto>;