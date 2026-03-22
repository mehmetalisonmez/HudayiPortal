using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Commands.CreateMaliIslem;

public sealed class CreateMaliIslemCommandHandler : IRequestHandler<CreateMaliIslemCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateMaliIslemCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateMaliIslemCommand request, CancellationToken cancellationToken)
	{
		var maliIslem = new MaliIslem
		{
			YonId = request.YonId,
			Baslik = request.Baslik,
			Aciklama = request.Aciklama,
			Tutar = request.Tutar,
			IslemTarihi = request.IslemTarihi,
			IlgiliKullaniciId = request.IlgiliKullaniciId,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<MaliIslem>().AddAsync(maliIslem, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return maliIslem.Id;
	}
}
