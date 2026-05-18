using FluentValidation;

namespace HudayiPortal.Application.Features.Duyurular.Commands.CreateDuyuru;

public sealed class CreateDuyuruCommandValidator
	: AbstractValidator<CreateDuyuruCommand>
{
	public CreateDuyuruCommandValidator()
	{
		RuleFor(x => x.Baslik)
			.NotEmpty().WithMessage("Başlık alanı boş olamaz.")
			.MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

		RuleFor(x => x.Icerik)
			.NotEmpty().WithMessage("İçerik alanı boş olamaz.");
	}
}
