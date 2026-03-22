using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekYorumlari.Commands.CreateYemekYorum;

public sealed class CreateYemekYorumCommandHandler : IRequestHandler<CreateYemekYorumCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public CreateYemekYorumCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(CreateYemekYorumCommand request, CancellationToken cancellationToken)
	{
		var yorum = new YemekYorumu
		{
			YemekMenuId = request.YemekMenuId,
			KullaniciId = _currentUserService.UserId,
			YorumMetni = request.YorumMetni,
			Puan = request.Puan,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<YemekYorumu>().AddAsync(yorum, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return yorum.Id;
	}
}
