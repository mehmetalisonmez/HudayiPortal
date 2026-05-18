using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Commands.DeleteMaliIslem;

public sealed class DeleteMaliIslemCommandHandler : IRequestHandler<DeleteMaliIslemCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteMaliIslemCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(DeleteMaliIslemCommand request, CancellationToken cancellationToken)
	{
		var maliIslem = await _unitOfWork.Repository<MaliIslem>()
			.GetByIdAsync(request.Id, cancellationToken)
			?? throw new KeyNotFoundException($"Mali işlem bulunamadı. Id: {request.Id}");

		maliIslem.SilindiMi = true;

		_unitOfWork.Repository<MaliIslem>().Update(maliIslem);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
