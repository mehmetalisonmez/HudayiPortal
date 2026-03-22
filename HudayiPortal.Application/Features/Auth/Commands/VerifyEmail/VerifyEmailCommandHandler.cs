using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HudayiPortal.Application.Features.Auth.Commands.VerifyEmail;

public sealed class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
{
	private readonly IUnitOfWork _unitOfWork;

	public VerifyEmailCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
	{
		string email;
		try
		{
			var decodedBytes = Convert.FromBase64String(request.Token);
			email = Encoding.UTF8.GetString(decodedBytes);
		}
		catch (FormatException)
		{
			throw new InvalidOperationException("Geçersiz doğrulama tokenı.");
		}

		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.Email == email && k.SilindiMi != true)
			.FirstOrDefaultAsync(cancellationToken);

		if (kullanici is null)
		{
			throw new InvalidOperationException("Kullanıcı bulunamadı.");
		}

		kullanici.EmailDogrulandiMi = true;
		kullanici.AktifMi = true;
		kullanici.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Kullanici>().Update(kullanici);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return true;
	}
}
