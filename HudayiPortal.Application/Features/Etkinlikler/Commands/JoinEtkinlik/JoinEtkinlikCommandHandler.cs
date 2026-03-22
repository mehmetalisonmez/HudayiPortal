using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.JoinEtkinlik;

public sealed class JoinEtkinlikCommandHandler : IRequestHandler<JoinEtkinlikCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public JoinEtkinlikCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(JoinEtkinlikCommand request, CancellationToken cancellationToken)
	{
		var kullaniciId = _currentUserService.UserId;

		var zatenKayitli = await _unitOfWork.Repository<EtkinlikKatilimcisi>()
			.AnyAsync(k => k.EtkinlikId == request.EtkinlikId
				&& k.KullaniciId == kullaniciId
				&& k.SilindiMi != true,
				cancellationToken);

		if (zatenKayitli)
		{
			throw new InvalidOperationException("Bu etkinliğe zaten kayıt oldunuz.");
		}

		var katilim = new EtkinlikKatilimcisi
		{
			EtkinlikId = request.EtkinlikId,
			KullaniciId = kullaniciId,
			KatilimDurumu = null,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<EtkinlikKatilimcisi>().AddAsync(katilim, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return katilim.Id;
	}
}
