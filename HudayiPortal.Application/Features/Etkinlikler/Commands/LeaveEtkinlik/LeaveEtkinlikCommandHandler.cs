using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.LeaveEtkinlik;

public sealed class LeaveEtkinlikCommandHandler : IRequestHandler<LeaveEtkinlikCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public LeaveEtkinlikCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(LeaveEtkinlikCommand request, CancellationToken cancellationToken)
	{
		var kullaniciId = _currentUserService.UserId;

		var katilim = await _unitOfWork.Repository<EtkinlikKatilimcisi>()
			.Where(k => k.EtkinlikId == request.EtkinlikId
				&& k.KullaniciId == kullaniciId
				&& k.SilindiMi != true)
			.FirstOrDefaultAsync(cancellationToken);

		if (katilim is null)
			throw new ValidationException("Kayıt bulunamadı.", new List<string> { "Bu etkinliğe kayıtlı değilsiniz." });

		// Soft delete
		katilim.SilindiMi = true;

		_unitOfWork.Repository<EtkinlikKatilimcisi>().Update(katilim);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
