using FluentValidation;

namespace HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetGrubu;

public sealed class CreateSohbetGrubuCommandValidator : AbstractValidator<CreateSohbetGrubuCommand>
{
	public CreateSohbetGrubuCommandValidator()
	{
		RuleFor(x => x.GrupAdi)
			.NotEmpty().WithMessage("Grup adı boş olamaz.")
			.MaximumLength(150).WithMessage("Grup adı en fazla 150 karakter olabilir.");

		RuleFor(x => x.SorumluHocaAdi)
			.NotEmpty().WithMessage("Sorumlu hoca adı boş olamaz.");

		RuleFor(x => x.Donem)
			.NotEmpty().WithMessage("Dönem boş olamaz.");
	}
}
