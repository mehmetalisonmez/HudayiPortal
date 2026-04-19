using FluentValidation;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.UpdateKullanici;

public sealed class UpdateKullaniciCommandValidator : AbstractValidator<UpdateKullaniciCommand>
{
	public UpdateKullaniciCommandValidator()
	{
		RuleFor(x => x.Id)
			.GreaterThan(0)
			.WithMessage("Geçerli bir kullanıcı ID'si gereklidir.");

		RuleFor(x => x.Ad)
			.NotEmpty().WithMessage("Ad alanı boş olamaz.")
			.MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

		RuleFor(x => x.Soyad)
			.NotEmpty().WithMessage("Soyad alanı boş olamaz.")
			.MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

		RuleFor(x => x.TcKimlikNo)
			.Length(11).WithMessage("TC Kimlik No tam olarak 11 karakter olmalıdır.")
			.Matches(@"^\d{11}$").WithMessage("TC Kimlik No yalnızca rakamlardan oluşmalıdır.")
			.When(x => !string.IsNullOrEmpty(x.TcKimlikNo));

		RuleFor(x => x.Telefon)
			.MaximumLength(15).WithMessage("Telefon en fazla 15 karakter olabilir.")
			.When(x => !string.IsNullOrEmpty(x.Telefon));

		RuleFor(x => x.Email)
			.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
			.MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.")
			.When(x => !string.IsNullOrEmpty(x.Email));

		RuleFor(x => x.KanGrubu)
			.MaximumLength(10).WithMessage("Kan grubu en fazla 10 karakter olabilir.")
			.When(x => !string.IsNullOrEmpty(x.KanGrubu));

		RuleFor(x => x.DogumTarihi)
			.LessThan(DateTime.Now).WithMessage("Doğum tarihi bugünden önce olmalıdır.")
			.When(x => x.DogumTarihi.HasValue);
	}
}
