using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetGrubu;

public sealed record CreateSohbetGrubuCommand(
	string GrupAdi,
	string SorumluHocaAdi,
	string Donem
) : IRequest<int>;
