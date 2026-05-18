using HudayiPortal.Domain.Enums;
using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.CreatePersonelNobet;

public sealed record CreatePersonelNobetCommand(
	int PersonelId,
	DateOnly Tarih,
	NobetTuru NobetTuru,
	string? Aciklama
) : IRequest<int>;
