using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Commands.UpdateYemekTanimi;

public sealed record UpdateYemekTanimiCommand(
	int Id,
	string YemekAdi,
	int KategoriId,
	int? Kalori,
	string? ResimUrl
) : IRequest<Unit>;
