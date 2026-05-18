using HudayiPortal.Domain.Repositories;
using MediatR;
using IslemKategorileriEntity = HudayiPortal.Domain.Entities.IslemKategorileri;

namespace HudayiPortal.Application.Features.IslemKategorileri.Commands.UpdateIslemKategorisi;

public sealed class UpdateIslemKategorisiCommandHandler : IRequestHandler<UpdateIslemKategorisiCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateIslemKategorisiCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(UpdateIslemKategorisiCommand request, CancellationToken cancellationToken)
	{
		var kategori = await _unitOfWork.Repository<IslemKategorileriEntity>()
			.GetByIdAsync(request.Id, cancellationToken)
			?? throw new KeyNotFoundException($"Kategori bulunamadı. Id: {request.Id}");

		kategori.KategoriAdi = request.KategoriAdi;
		kategori.YonId = request.YonId;

		_unitOfWork.Repository<IslemKategorileriEntity>().Update(kategori);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
