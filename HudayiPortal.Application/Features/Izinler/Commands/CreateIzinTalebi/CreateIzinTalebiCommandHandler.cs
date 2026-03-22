using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Commands.CreateIzinTalebi;

public sealed class CreateIzinTalebiCommandHandler : IRequestHandler<CreateIzinTalebiCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public CreateIzinTalebiCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(CreateIzinTalebiCommand request, CancellationToken cancellationToken)
	{
		var izin = new Izin
		{
			KullaniciId = _currentUserService.UserId,
			IzinTurId = request.IzinTurId,
			BaslangicTarihi = request.BaslangicTarihi,
			BitisTarihi = request.BitisTarihi,
			GidecegiAdres = request.GidecegiAdres,
			Aciklama = request.Aciklama,
			OnayDurumu = 0,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<Izin>().AddAsync(izin, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return izin.Id;
	}
}
