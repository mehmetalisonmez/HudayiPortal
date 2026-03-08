using AutoMapper;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.CreateKullanici;

public sealed class CreateKullaniciCommandHandler : IRequestHandler<CreateKullaniciCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateKullaniciCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<int> Handle(CreateKullaniciCommand request, CancellationToken cancellationToken)
	{
		var kullanici = _mapper.Map<Kullanici>(request);

		// Assign default values not covered by mapping
		kullanici.AktifMi = true;
		kullanici.EmailDogrulandiMi = false;
		kullanici.SilindiMi = false;

		// Hash the password if provided
		if (!string.IsNullOrEmpty(request.Sifre))
		{
			kullanici.SifreHash = BCrypt.Net.BCrypt.HashPassword(request.Sifre);
		}

		await _unitOfWork.Repository<Kullanici>().AddAsync(kullanici, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return kullanici.Id;
	}
}