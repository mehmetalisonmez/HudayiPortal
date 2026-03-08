using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.ExportAylikYemekMenu;

public sealed record ExportAylikYemekMenuQuery(
	int Yil,
	int Ay
) : IRequest<byte[]>;
