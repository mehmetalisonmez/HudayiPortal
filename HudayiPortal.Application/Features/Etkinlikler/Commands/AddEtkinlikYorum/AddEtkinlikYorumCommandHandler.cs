using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.AddEtkinlikYorum;

public sealed class AddEtkinlikYorumCommandHandler : IRequestHandler<AddEtkinlikYorumCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public AddEtkinlikYorumCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(AddEtkinlikYorumCommand request, CancellationToken cancellationToken)
	{
		var etkinlikVar = await _unitOfWork.Repository<Etkinlik>()
			.AnyAsync(e => e.Id == request.EtkinlikId && e.SilindiMi != true, cancellationToken);

		if (!etkinlikVar)
			throw new ValidationException("Etkinlik bulunamadı.", new List<string> { $"Id={request.EtkinlikId} olan etkinlik mevcut değil." });

		var yorum = new EtkinlikYorumu
		{
			EtkinlikId = request.EtkinlikId,
			KullaniciId = _currentUserService.UserId,
			YorumMetni = request.YorumMetni,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<EtkinlikYorumu>().AddAsync(yorum, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return yorum.Id;
	}
}
