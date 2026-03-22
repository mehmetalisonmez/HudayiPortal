using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.CreateEtkinlik;

public sealed class CreateEtkinlikCommandHandler : IRequestHandler<CreateEtkinlikCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public CreateEtkinlikCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(CreateEtkinlikCommand request, CancellationToken cancellationToken)
	{
		var etkinlik = new Etkinlik
		{
			Baslik = request.Baslik,
			Aciklama = request.Aciklama,
			BaslangicTarihi = request.BaslangicTarihi,
			BitisTarihi = request.BitisTarihi,
			SonKayitTarihi = request.SonKayitTarihi,
			Ucret = request.Ucret,
			ZorunluMu = request.ZorunluMu,
			ResimUrl = request.ResimUrl,
			OlusturanPersonelId = _currentUserService.UserId,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<Etkinlik>().AddAsync(etkinlik, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return etkinlik.Id;
	}
}
