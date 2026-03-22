using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Commands.UpdateIzinDurumu;

public sealed class UpdateIzinDurumuCommandHandler : IRequestHandler<UpdateIzinDurumuCommand, bool>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public UpdateIzinDurumuCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<bool> Handle(UpdateIzinDurumuCommand request, CancellationToken cancellationToken)
	{
		var izin = await _unitOfWork.Repository<Izin>()
			.GetByIdAsync(request.IzinId, cancellationToken);

		if (izin is null || izin.SilindiMi == true)
		{
			return false;
		}

		izin.OnayDurumu = request.YeniDurum;
		izin.OnaylayanPersonelId = _currentUserService.UserId;
		izin.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Izin>().Update(izin);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return true;
	}
}
