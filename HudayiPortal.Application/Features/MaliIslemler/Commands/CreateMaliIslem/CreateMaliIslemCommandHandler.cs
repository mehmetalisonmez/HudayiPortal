using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HudayiPortal.Application.Features.MaliIslemler.Commands.CreateMaliIslem;

public sealed class CreateMaliIslemCommandHandler : IRequestHandler<CreateMaliIslemCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ILogger<CreateMaliIslemCommandHandler> _logger;

	public CreateMaliIslemCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateMaliIslemCommandHandler> logger)
	{
		_unitOfWork = unitOfWork;
		_logger = logger;
	}

	public async Task<int> Handle(CreateMaliIslemCommand request, CancellationToken cancellationToken)
	{
		var maliIslem = new MaliIslem
		{
			YonId = request.YonId,
			Baslik = request.Baslik,
			Aciklama = request.Aciklama,
			Tutar = request.Tutar,
			IslemTarihi = request.IslemTarihi,
			IlgiliKullaniciId = request.IlgiliKullaniciId,
			KategoriId = request.KategoriId,
			BelgeUrl = request.BelgeUrl,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<MaliIslem>().AddAsync(maliIslem, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		_logger.LogInformation("Mali işlem kaydı girildi. IslemId: {IslemId}, Tutar: {Tutar}, YonId: {YonId}", maliIslem.Id, request.Tutar, request.YonId);

		return maliIslem.Id;
	}
}
