using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HudayiPortal.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IEmailService _emailService;

	public RegisterCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService)
	{
		_unitOfWork = unitOfWork;
		_emailService = emailService;
	}

	public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		var emailExists = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.Email == request.Email && k.SilindiMi != true)
			.AnyAsync(cancellationToken);

		if (emailExists)
		{
			throw new InvalidOperationException("Bu e-posta adresi zaten kayıtlı.");
		}

		var kullanici = new Kullanici
		{
			RolId = 1,
			Ad = request.Ad,
			Soyad = request.Soyad,
			TcKimlikNo = request.TcKimlikNo,
			Telefon = request.Telefon,
			Email = request.Email,
			SifreHash = BCrypt.Net.BCrypt.HashPassword(request.Sifre),
			AktifMi = false,
			EmailDogrulandiMi = false,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<Kullanici>().AddAsync(kullanici, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Email));
		var verificationLink = $"http://localhost:7288/verify-email?token={encodedToken}";
		await _emailService.SendVerificationEmailAsync(request.Email, verificationLink);

		return kullanici.Id;
	}
}
