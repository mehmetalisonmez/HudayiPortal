using FluentValidation;

namespace HudayiPortal.Application.Features.Yoklamalar.Commands.TakeAttendance;

public sealed class TakeAttendanceCommandValidator : AbstractValidator<TakeAttendanceCommand>
{
	public TakeAttendanceCommandValidator()
	{
		RuleFor(x => x.YoklamaTurId)
			.GreaterThan(0)
			.WithMessage("Yoklama türü seçilmelidir.");

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
