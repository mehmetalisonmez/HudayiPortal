using FluentValidation;

namespace HudayiPortal.Application.Features.Sikayetler.Commands.CreateSikayet;

public sealed class CreateSikayetCommandValidator
	: AbstractValidator<CreateSikayetCommand>
{
	public CreateSikayetCommandValidator()
	{
		RuleFor(x => x.Baslik)
			.NotEmpty().WithMessage("Başlık alanı boş olamaz.")
			.MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

		RuleFor(x => x.Icerik)
			.NotEmpty().WithMessage("İçerik alanı boş olamaz.")
			.MaximumLength(2000).WithMessage("İçerik en fazla 2000 karakter olabilir.");
	}
}
