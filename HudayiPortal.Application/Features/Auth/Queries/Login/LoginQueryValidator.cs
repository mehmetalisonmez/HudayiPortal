using FluentValidation;

namespace HudayiPortal.Application.Features.Auth.Queries.Login;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
	public LoginQueryValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("E-posta adresi boþ olamaz.")
			.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

		RuleFor(x => x.Sifre)
			.NotEmpty().WithMessage("Þifre boþ olamaz.");
	}
}