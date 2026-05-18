using FluentValidation;

namespace HudayiPortal.Application.Features.SohbetYoklamalar.Commands.TakeSohbetAttendance;

public sealed class TakeSohbetAttendanceCommandValidator : AbstractValidator<TakeSohbetAttendanceCommand>
{
	public TakeSohbetAttendanceCommandValidator()
	{
		RuleFor(x => x.SohbetId)
			.GreaterThan(0)
			.WithMessage("Sohbet oturumu seçilmelidir.");

		RuleFor(x => x.Ogrenciler)
			.NotEmpty()
			.WithMessage("Öğrenci listesi boş olamaz.");

		RuleForEach(x => x.Ogrenciler).ChildRules(o =>
		{
			o.RuleFor(s => s.KullaniciId)
				.GreaterThan(0)
				.WithMessage("Geçersiz öğrenci bilgisi.");
		});
	}
}
