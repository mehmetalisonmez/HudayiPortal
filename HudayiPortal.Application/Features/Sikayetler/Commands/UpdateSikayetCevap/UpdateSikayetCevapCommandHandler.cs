using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Sikayetler.Commands.UpdateSikayetCevap;

public sealed class UpdateSikayetCevapCommandHandler : IRequestHandler<UpdateSikayetCevapCommand, bool>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateSikayetCevapCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<bool> Handle(UpdateSikayetCevapCommand request, CancellationToken cancellationToken)
	{
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
