using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Mesajlar.Commands.CreateMesaj;

public sealed class CreateMesajCommandHandler : IRequestHandler<CreateMesajCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public CreateMesajCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(CreateMesajCommand request, CancellationToken cancellationToken)
	{
		var mesaj = new Mesaj
		{
			GonderenId = _currentUserService.UserId,
			AliciId = request.AliciId,
			ChatGrupId = request.ChatGrupId,
			MesajIcerigi = request.MesajIcerigi,
			OkunduMu = false,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<Mesaj>().AddAsync(mesaj, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return mesaj.Id;
	}
}
