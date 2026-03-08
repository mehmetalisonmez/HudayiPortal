namespace HudayiPortal.Application.Features.Yoklamalar.Commands.TakeAttendance;

public sealed record StudentAttendanceDto(
	int KullaniciId,
	bool Durum,
	string? Aciklama
);
