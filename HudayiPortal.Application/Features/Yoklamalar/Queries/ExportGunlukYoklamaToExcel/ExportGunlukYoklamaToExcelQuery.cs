using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.ExportGunlukYoklamaToExcel;

public sealed record ExportGunlukYoklamaToExcelQuery(
	DateOnly StartDate,
	DateOnly EndDate,
	int YoklamaTurId
) : IRequest<byte[]>;
