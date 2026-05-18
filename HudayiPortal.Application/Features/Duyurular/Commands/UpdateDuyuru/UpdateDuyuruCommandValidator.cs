using FluentValidation;

namespace HudayiPortal.Application.Features.Duyurular.Commands.UpdateDuyuru;

public sealed class UpdateDuyuruCommandValidator
	: AbstractValidator<UpdateDuyuruCommand>
{
	public UpdateDuyuruCommandValidator()
	{
		RuleFor(x => x.Id)
			.GreaterThan(0).WithMessage("Geçerli bir duyuru Id'si girilmelidir.");

		RuleFor(x => x.Baslik)
			.NotEmpty().WithMessage("Başlık alanı boş olamaz.")
			.MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

		RuleFor(x => x.Icerik)
			.NotEmpty().WithMessage("İçerik alanı boş olamaz.");
	}
}
