using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Commands.CreateDuyuru;

public sealed class CreateDuyuruCommandHandler : IRequestHandler<CreateDuyuruCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateDuyuruCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateDuyuruCommand request, CancellationToken cancellationToken)
	{
		var duyuru = new Duyuru
		{
			OlusturanKullaniciId = 1,
			Baslik = request.Baslik,
			Icerik = request.Icerik,
			GecerlilikTarihi = request.GecerlilikTarihi,
			OlusturulmaTarihi = DateTime.UtcNow,
            HedefRolId = request.HedefRolId,
            SilindiMi = false
		};

		await _unitOfWork.Repository<Duyuru>().AddAsync(duyuru, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return duyuru.Id;
	}
}
