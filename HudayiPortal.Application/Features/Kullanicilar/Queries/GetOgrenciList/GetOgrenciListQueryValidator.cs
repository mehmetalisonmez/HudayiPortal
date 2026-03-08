using FluentValidation;

namespace HudayiPortal.Application.Features.Kullanicilar.Queries.GetOgrenciList;

public sealed class GetOgrenciListQueryValidator : AbstractValidator<GetOgrenciListQuery>
{
	public GetOgrenciListQueryValidator()
	{
		RuleFor(x => x.PageNumber)
			.GreaterThanOrEqualTo(1)
			.WithMessage("Sayfa numarasý en az 1 olmalýdýr.");

		RuleFor(x => x.PageSize)
			.InclusiveBetween(1, 100)
			.WithMessage("Sayfa boyutu 1 ile 100 arasýnda olmalýdýr.");
	}
}