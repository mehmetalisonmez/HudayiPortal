using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HudayiPortal.Application.Features.Sikayetler.Commands.CreateSikayet;

public sealed class CreateSikayetCommandHandler : IRequestHandler<CreateSikayetCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;
	private readonly ILogger<CreateSikayetCommandHandler> _logger;

	public CreateSikayetCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ILogger<CreateSikayetCommandHandler> logger)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
		_logger = logger;
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

		_logger.LogInformation("Yeni şikayet kaydı oluşturuldu. ŞikayetId: {SikayetId}, GonderenKullaniciId: {UserId}", sikayet.Id, _currentUserService.UserId);

		return sikayet.Id;
	}
}
