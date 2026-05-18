using FluentValidation;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.UpdatePersonelNobet;

public sealed class UpdatePersonelNobetCommandValidator : AbstractValidator<UpdatePersonelNobetCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdatePersonelNobetCommandValidator(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;

		RuleFor(x => x.Id)
			.GreaterThan(0).WithMessage("Geçerli bir nöbet kaydı seçiniz.");

		RuleFor(x => x.PersonelId)
			.GreaterThan(0).WithMessage("Geçerli bir personel seçiniz.");

		RuleFor(x => x.Tarih)
			.NotEmpty().WithMessage("Tarih boş olamaz.");

		RuleFor(x => x.NobetTuru)
			.IsInEnum().WithMessage("Geçerli bir nöbet türü seçiniz.");

		RuleFor(x => x)
			.MustAsync(async (cmd, ct) =>
				!await _unitOfWork.Repository<PersonelNobeti>().AnyAsync(
					n => n.PersonelId == cmd.PersonelId
					  && n.Tarih == cmd.Tarih
					  && n.SilindiMi != true
					  && n.Id != cmd.Id, ct))
			.WithMessage("Bu personel için bu tarihte zaten bir nöbet kaydı bulunmaktadır.");
	}
}
