using FluentValidation;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.CreateKullanici;

public sealed class CreateKullaniciCommandValidator : AbstractValidator<CreateKullaniciCommand>
{
	public CreateKullaniciCommandValidator()
	{
		RuleFor(x => x.RolId)
			.GreaterThan(0)
			.WithMessage("Rol seçilmelidir.");

		RuleFor(x => x.Ad)
			.NotEmpty().WithMessage("Ad alaný boþ olamaz.")
			.MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

		RuleFor(x => x.Soyad)
			.NotEmpty().WithMessage("Soyad alaný boþ olamaz.")
			.MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

		RuleFor(x => x.TcKimlikNo)
			.Length(11).WithMessage("TC Kimlik No tam olarak 11 karakter olmalýdýr.")
			.Matches(@"^\d{11}$").WithMessage("TC Kimlik No yalnýzca rakamlardan oluþmalýdýr.")
			.When(x => !string.IsNullOrEmpty(x.TcKimlikNo));

		RuleFor(x => x.Telefon)
			.MaximumLength(15).WithMessage("Telefon en fazla 15 karakter olabilir.")
			.When(x => !string.IsNullOrEmpty(x.Telefon));

		RuleFor(x => x.Email)
			.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
			.MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.")
			.When(x => !string.IsNullOrEmpty(x.Email));

		RuleFor(x => x.Sifre)
			.MinimumLength(6).WithMessage("Þifre en az 6 karakter olmalýdýr.")
			.MaximumLength(100).WithMessage("Þifre en fazla 100 karakter olabilir.")
			.When(x => !string.IsNullOrEmpty(x.Sifre));

		RuleFor(x => x.KanGrubu)
			.MaximumLength(10).WithMessage("Kan grubu en fazla 10 karakter olabilir.")
			.When(x => !string.IsNullOrEmpty(x.KanGrubu));

		RuleFor(x => x.DogumTarihi)
			.LessThan(DateTime.Now).WithMessage("Doðum tarihi bugünden önce olmalýdýr.")
			.When(x => x.DogumTarihi.HasValue);
	}
}