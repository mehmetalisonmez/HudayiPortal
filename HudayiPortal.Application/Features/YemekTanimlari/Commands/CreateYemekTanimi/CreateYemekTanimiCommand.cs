using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Commands.CreateYemekTanimi;

public sealed record CreateYemekTanimiCommand(
	string YemekAdi,
	int KategoriId,
	int? Kalori,
	string? ResimUrl
) : IRequest<int>;
