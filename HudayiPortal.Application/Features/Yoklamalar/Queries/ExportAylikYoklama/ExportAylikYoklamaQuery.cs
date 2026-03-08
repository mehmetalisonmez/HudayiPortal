using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.ExportAylikYoklama;

public sealed record ExportAylikYoklamaQuery(
	int Yil,
	int Ay,
	int YoklamaTurId
) : IRequest<byte[]>;
