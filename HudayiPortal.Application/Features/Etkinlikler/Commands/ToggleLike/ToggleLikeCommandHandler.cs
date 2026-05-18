using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.ToggleLike;

public sealed class ToggleLikeCommandHandler : IRequestHandler<ToggleLikeCommand, bool>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public ToggleLikeCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<bool> Handle(ToggleLikeCommand request, CancellationToken cancellationToken)
	{
		var kullaniciId = _currentUserService.UserId;

		var mevcutBegeni = await _unitOfWork.Repository<EtkinlikBegeni>()
			.Where(b => b.EtkinlikId == request.EtkinlikId && b.KullaniciId == kullaniciId)
			.FirstOrDefaultAsync(cancellationToken);

		if (mevcutBegeni is not null)
		{
			// Unlike — beğeniyi kaldır
			_unitOfWork.Repository<EtkinlikBegeni>().Remove(mevcutBegeni);
			await _unitOfWork.SaveChangesAsync(cancellationToken);
			return false;
		}

		// Like — yeni beğeni ekle
		var begeni = new EtkinlikBegeni
		{
			EtkinlikId = request.EtkinlikId,
			KullaniciId = kullaniciId,
			OlusturulmaTarihi = DateTime.UtcNow
		};

		await _unitOfWork.Repository<EtkinlikBegeni>().AddAsync(begeni, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return true;
	}
}
