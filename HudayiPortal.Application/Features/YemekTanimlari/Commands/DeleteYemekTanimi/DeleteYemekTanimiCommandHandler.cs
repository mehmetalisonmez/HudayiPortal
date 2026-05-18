using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Commands.DeleteYemekTanimi;

public sealed class DeleteYemekTanimiCommandHandler : IRequestHandler<DeleteYemekTanimiCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteYemekTanimiCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteYemekTanimiCommand request, CancellationToken cancellationToken)
	{
		var entity = await _unitOfWork.Repository<YemekTanimi>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (entity is null)
			throw new KeyNotFoundException($"Yemek tanımı bulunamadı: {request.Id}");

		entity.SilindiMi = true;

		_unitOfWork.Repository<YemekTanimi>().Update(entity);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
