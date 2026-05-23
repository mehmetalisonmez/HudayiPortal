using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Duyurular.Commands.CreateDuyuru;

public sealed class CreateDuyuruCommandHandler : IRequestHandler<CreateDuyuruCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;
	private readonly ICacheService _cacheService;

	public CreateDuyuruCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ICacheService cacheService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
		_cacheService = cacheService;
	}

	public async Task<int> Handle(CreateDuyuruCommand request, CancellationToken cancellationToken)
	{
		var duyuru = new Duyuru
		{
			OlusturanKullaniciId = _currentUserService.UserId,
			Baslik = request.Baslik,
			Icerik = request.Icerik,
			YayinTarihi = request.YayinTarihi,
			GecerlilikTarihi = request.GecerlilikTarihi,
			HedefRolId = request.HedefRolId
		};

		await _unitOfWork.Repository<Duyuru>().AddAsync(duyuru, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		// Cache Invalidation
		await _cacheService.RemoveAsync("duyurular:aktif", cancellationToken);

		return duyuru.Id;
	}
}
