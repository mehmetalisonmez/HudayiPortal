using FluentValidation;

namespace HudayiPortal.Application.Features.Sohbet.Commands.SyncOgrenciler;

public sealed class SyncOgrencilerCommandValidator : AbstractValidator<SyncOgrencilerCommand>
{
	public SyncOgrencilerCommandValidator()
	{
		RuleFor(x => x.SohbetGrupId)
			.GreaterThan(0)
			.WithMessage("Geçersiz grup bilgisi.");

		RuleFor(x => x.KullaniciIds)
			.NotNull()
			.WithMessage("Öğrenci listesi boş olamaz.");
	}
}
