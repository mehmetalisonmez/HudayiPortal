using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetAvailableOgrenciler;

public sealed record GetAvailableOgrencilerQuery(int SohbetGrupId) : IRequest<List<AvailableOgrenciDto>>;
