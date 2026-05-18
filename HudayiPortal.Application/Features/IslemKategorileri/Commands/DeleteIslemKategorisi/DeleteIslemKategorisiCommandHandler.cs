using HudayiPortal.Domain.Repositories;
using MediatR;
using IslemKategorileriEntity = HudayiPortal.Domain.Entities.IslemKategorileri;

namespace HudayiPortal.Application.Features.IslemKategorileri.Commands.DeleteIslemKategorisi;

public sealed class DeleteIslemKategorisiCommandHandler : IRequestHandler<DeleteIslemKategorisiCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteIslemKategorisiCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(DeleteIslemKategorisiCommand request, CancellationToken cancellationToken)
	{
		var kategori = await _unitOfWork.Repository<IslemKategorileriEntity>()
			.GetByIdAsync(request.Id, cancellationToken)
			?? throw new KeyNotFoundException($"Kategori bulunamadı. Id: {request.Id}");

		kategori.SilindiMi = true;

		_unitOfWork.Repository<IslemKategorileriEntity>().Update(kategori);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
