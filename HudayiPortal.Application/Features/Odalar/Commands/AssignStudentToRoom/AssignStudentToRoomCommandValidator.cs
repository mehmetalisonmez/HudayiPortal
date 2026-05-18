using FluentValidation;

namespace HudayiPortal.Application.Features.Odalar.Commands.AssignStudentToRoom;

public sealed class AssignStudentToRoomCommandValidator
	: AbstractValidator<AssignStudentToRoomCommand>
{
	public AssignStudentToRoomCommandValidator()
	{
		RuleFor(x => x.KullaniciId)
			.GreaterThan(0)
			.WithMessage("Geçerli bir kullanıcı ID'si gereklidir.");

		RuleFor(x => x.OdaId)
			.GreaterThan(0)
			.WithMessage("Geçerli bir oda ID'si gereklidir.")
			.When(x => x.OdaId.HasValue);
	}
}
