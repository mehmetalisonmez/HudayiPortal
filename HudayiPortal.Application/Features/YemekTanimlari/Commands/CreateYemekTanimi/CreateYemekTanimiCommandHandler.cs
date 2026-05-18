using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Commands.CreateYemekTanimi;

public sealed class CreateYemekTanimiCommandHandler : IRequestHandler<CreateYemekTanimiCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateYemekTanimiCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateYemekTanimiCommand request, CancellationToken cancellationToken)
	{
		var entity = new YemekTanimi
		{
			YemekAdi = request.YemekAdi,
			KategoriId = request.KategoriId,
			Kalori = request.Kalori,
			ResimUrl = request.ResimUrl,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<YemekTanimi>().AddAsync(entity, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return entity.Id;
	}
}
