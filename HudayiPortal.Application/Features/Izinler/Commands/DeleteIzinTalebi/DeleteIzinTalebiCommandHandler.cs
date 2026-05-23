using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Izinler.Commands.DeleteIzinTalebi;

public sealed class DeleteIzinTalebiCommandHandler : IRequestHandler<DeleteIzinTalebiCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public DeleteIzinTalebiCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(DeleteIzinTalebiCommand request, CancellationToken cancellationToken)
	{
		var izin = await _unitOfWork.Repository<Izin>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (izin is null)
			throw new KeyNotFoundException($"İzin talebi bulunamadı: {request.Id}");

		var currentUserId = _currentUserService.UserId;
		var roleName = _currentUserService.Role;

		if (roleName != "Admin" && roleName != "Personel" && izin.KullaniciId != currentUserId)
		{
			throw new BusinessException("Bu işlem için yetkiniz bulunmamaktadır.");
		}

		// Soft Delete — fiziksel silme yerine bayrak ile işaretleme
		izin.SilindiMi = true;
		izin.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Izin>().Update(izin);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
