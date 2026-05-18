using HudayiPortal.Domain.Enums;
using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.UpdatePersonelNobet;

public sealed record UpdatePersonelNobetCommand(
	int Id,
	int PersonelId,
	DateOnly Tarih,
	NobetTuru NobetTuru,
	string? Aciklama
) : IRequest<Unit>;
