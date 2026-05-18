using FluentValidation;

namespace HudayiPortal.Application.Features.IslemKategorileri.Commands.CreateIslemKategorisi;

public class CreateIslemKategorisiCommandValidator : AbstractValidator<CreateIslemKategorisiCommand>
{
	public CreateIslemKategorisiCommandValidator()
	{
		RuleFor(x => x.KategoriAdi)
			.NotEmpty().WithMessage("Kategori adı zorunludur.")
			.MaximumLength(100).WithMessage("Kategori adı 100 karakterden uzun olamaz.");

		RuleFor(x => x.YonId)
			.GreaterThan(0).WithMessage("Geçerli bir yön seçiniz.");
	}
}
