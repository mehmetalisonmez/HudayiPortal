using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Commands.TakeAttendance;

public sealed record TakeAttendanceCommand(
	int YoklamaTurId,
	DateOnly Tarih,
	List<StudentAttendanceDto> Ogrenciler
) : IRequest<int>;
