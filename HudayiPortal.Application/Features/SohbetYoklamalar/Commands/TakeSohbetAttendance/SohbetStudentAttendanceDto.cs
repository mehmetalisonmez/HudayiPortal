namespace HudayiPortal.Application.Features.SohbetYoklamalar.Commands.TakeSohbetAttendance;

public sealed record SohbetStudentAttendanceDto(
	int KullaniciId,
	bool KatilimDurumu,
	string? MazeretAciklamasi
);
