using HudayiPortal.Domain.Repositories;
using MediatR;
using IslemKategorileriEntity = HudayiPortal.Domain.Entities.IslemKategorileri;

namespace HudayiPortal.Application.Features.IslemKategorileri.Commands.CreateIslemKategorisi;

public sealed class CreateIslemKategorisiCommandHandler : IRequestHandler<CreateIslemKategorisiCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateIslemKategorisiCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateIslemKategorisiCommand request, CancellationToken cancellationToken)
	{
		var kategori = new IslemKategorileriEntity
		{
			KategoriAdi = request.KategoriAdi,
			YonId = request.YonId,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<IslemKategorileriEntity>().AddAsync(kategori, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return kategori.Id;
	}
}
