using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Commands.UpdateYemekTanimi;

public sealed class UpdateYemekTanimiCommandHandler : IRequestHandler<UpdateYemekTanimiCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateYemekTanimiCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateYemekTanimiCommand request, CancellationToken cancellationToken)
	{
		var entity = await _unitOfWork.Repository<YemekTanimi>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (entity is null)
			throw new KeyNotFoundException($"Yemek tanımı bulunamadı: {request.Id}");

		entity.YemekAdi = request.YemekAdi;
		entity.KategoriId = request.KategoriId;
		entity.Kalori = request.Kalori;
		entity.ResimUrl = request.ResimUrl;

		_unitOfWork.Repository<YemekTanimi>().Update(entity);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
