using FluentValidation;

namespace HudayiPortal.Application.Features.Sohbet.Commands.UpdateSohbetSession;

public sealed class UpdateSohbetSessionCommandValidator : AbstractValidator<UpdateSohbetSessionCommand>
{
	public UpdateSohbetSessionCommandValidator()
	{
		RuleFor(x => x.Id)
			.GreaterThan(0)
			.WithMessage("Geçersiz oturum bilgisi.");

		RuleFor(x => x.KonuBasligi)
			.NotEmpty().WithMessage("Konu başlığı boş olamaz.")
			.MaximumLength(300).WithMessage("Konu başlığı en fazla 300 karakter olabilir.");
	}
}
