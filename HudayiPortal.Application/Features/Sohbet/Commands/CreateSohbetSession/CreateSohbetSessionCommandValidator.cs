using FluentValidation;
using HudayiPortal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetSession;

public sealed class CreateSohbetSessionCommandValidator : AbstractValidator<CreateSohbetSessionCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateSohbetSessionCommandValidator(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;

		RuleFor(x => x.SohbetGrupId)
			.GreaterThan(0)
			.WithMessage("Geçersiz grup bilgisi.");

		RuleFor(x => x.KonuBasligi)
			.NotEmpty().WithMessage("Konu başlığı boş olamaz.")
			.MaximumLength(300).WithMessage("Konu başlığı en fazla 300 karakter olabilir.");

		RuleFor(x => x)
			.MustAsync(TarihGrupUnique)
			.WithMessage("Bu tarih ve grup için zaten bir oturum mevcut.");
	}

	private async Task<bool> TarihGrupUnique(CreateSohbetSessionCommand command, CancellationToken cancellationToken)
	{
		var tarihStart = command.Tarih.Date;
		var tarihEnd = tarihStart.AddDays(1);

		return !await _unitOfWork.Repository<SohbetEntity>()
			.Where(s => s.SohbetGrupId == command.SohbetGrupId
				&& s.Tarih >= tarihStart && s.Tarih < tarihEnd
				&& s.SilindiMi != true)
			.AnyAsync(cancellationToken);
	}
}
