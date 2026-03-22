using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Sikayetler.Commands.CreateSikayet;

public sealed class CreateSikayetCommandHandler : IRequestHandler<CreateSikayetCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public CreateSikayetCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(CreateSikayetCommand request, CancellationToken cancellationToken)
	{
		var sikayet = new Sikayet
		{
			GonderenKullaniciId = _currentUserService.UserId,
			Baslik = request.Baslik,
			Icerik = request.Icerik,
			Durum = 0,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<Sikayet>().AddAsync(sikayet, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return sikayet.Id;
	}
}
