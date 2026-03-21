using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Commands.CreateIzinTalebi;

public sealed record CreateIzinTalebiCommand(
	int IzinTurId,
	DateTime BaslangicTarihi,
	DateTime BitisTarihi,
	string GidecegiAdres,
	string Aciklama
) : IRequest<int>;
