using HudayiPortal.Application.Exceptions;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.DeleteEtkinlik;

public sealed class DeleteEtkinlikCommandHandler : IRequestHandler<DeleteEtkinlikCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteEtkinlikCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteEtkinlikCommand request, CancellationToken cancellationToken)
	{
		var etkinlik = await _unitOfWork.Repository<Etkinlik>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (etkinlik is null || etkinlik.SilindiMi == true)
			throw new ValidationException("Etkinlik bulunamadı.", new List<string> { $"Id={request.Id} olan etkinlik mevcut değil." });

		etkinlik.SilindiMi = true;
		etkinlik.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Etkinlik>().Update(etkinlik);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
