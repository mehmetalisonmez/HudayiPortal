using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.CreatePersonelNobet;

public sealed record CreatePersonelNobetCommand(
	int PersonelId,
	DateTime Tarih,
	string NobetTuru,
	string Aciklama
) : IRequest<int>;
