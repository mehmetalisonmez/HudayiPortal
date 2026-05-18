using MediatR;

namespace HudayiPortal.Application.Features.SohbetYoklamalar.Commands.TakeSohbetAttendance;

public sealed record TakeSohbetAttendanceCommand(
	int SohbetId,
	List<SohbetStudentAttendanceDto> Ogrenciler
) : IRequest<int>;
