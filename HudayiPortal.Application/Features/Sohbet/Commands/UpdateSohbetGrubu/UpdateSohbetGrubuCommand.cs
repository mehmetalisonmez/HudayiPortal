using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.UpdateSohbetGrubu;

public sealed record UpdateSohbetGrubuCommand(
	int Id,
	string GrupAdi,
	string SorumluHocaAdi,
	string Donem
) : IRequest<Unit>;
