using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.DeleteYemekMenu;

public sealed class DeleteYemekMenuCommandHandler : IRequestHandler<DeleteYemekMenuCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteYemekMenuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteYemekMenuCommand request, CancellationToken cancellationToken)
	{
		var menu = await _unitOfWork.Repository<YemekMenusu>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (menu is null)
			throw new KeyNotFoundException($"Yemek menüsü bulunamadı: {request.Id}");

		// Soft Delete — fiziksel silme yerine bayrak ile işaretleme
		menu.SilindiMi = true;
		menu.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<YemekMenusu>().Update(menu);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
