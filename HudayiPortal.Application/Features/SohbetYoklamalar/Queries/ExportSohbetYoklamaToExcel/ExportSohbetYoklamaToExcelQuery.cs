using MediatR;

namespace HudayiPortal.Application.Features.SohbetYoklamalar.Queries.ExportSohbetYoklamaToExcel;

public sealed record ExportSohbetYoklamaToExcelQuery(
	DateOnly StartDate,
	DateOnly EndDate,
	int GrupId
) : IRequest<byte[]>;
