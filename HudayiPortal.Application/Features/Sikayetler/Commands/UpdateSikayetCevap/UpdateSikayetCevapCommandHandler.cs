using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Sikayetler.Commands.UpdateSikayetCevap;

public sealed class UpdateSikayetCevapCommandHandler : IRequestHandler<UpdateSikayetCevapCommand, bool>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public UpdateSikayetCevapCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<bool> Handle(UpdateSikayetCevapCommand request, CancellationToken cancellationToken)
	{
		var roleName = _currentUserService.Role;
		if (roleName != "Admin" && roleName != "Personel")
		{
			throw new BusinessException("Bu işlem için yetkiniz bulunmamaktadır.");
		}

		var sikayet = await _unitOfWork.Repository<Sikayet>()
			.GetByIdAsync(request.SikayetId, cancellationToken);

		if (sikayet is null || sikayet.SilindiMi == true)
		{
			return false;
		}

		sikayet.Cevap = request.Cevap;
		sikayet.Durum = request.YeniDurum;
		sikayet.CevaplanmaTarihi = DateTime.UtcNow;
		sikayet.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Sikayet>().Update(sikayet);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return true;
	}
}
