using System;
using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Auth.Queries.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICacheService _cacheService;
	private readonly IEmailService _emailService;

	public LoginQueryHandler(
		IUnitOfWork unitOfWork,
		ICacheService cacheService,
		IEmailService emailService)
	{
		_unitOfWork = unitOfWork;
		_cacheService = cacheService;
		_emailService = emailService;
	}

	public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.Email == request.Email && k.SilindiMi != true)
			.Include(k => k.Rol)
			.FirstOrDefaultAsync(cancellationToken);

		if (kullanici is null)
			throw new BusinessException("Lütfen bilgilerinizi kontrol ediniz.");

		if (string.IsNullOrEmpty(kullanici.SifreHash))
			throw new BusinessException("Lütfen bilgilerinizi kontrol ediniz.");

		var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Sifre, kullanici.SifreHash);

		if (!isPasswordValid)
			throw new BusinessException("Lütfen bilgilerinizi kontrol ediniz.");

		// Şifre doğru! OTP Üretimi
		var otpCode = new Random().Next(100000, 999999).ToString();
		
		// Redis üzerinde 3 dakika saklayalım
		var cacheKey = $"otp:{kullanici.Email}";
		await _cacheService.SetAsync(cacheKey, otpCode, TimeSpan.FromMinutes(3), cancellationToken);

		// E-posta gönderelim
		if (!string.IsNullOrEmpty(kullanici.Email))
		{
			await _emailService.SendOtpEmailAsync(kullanici.Email, otpCode);
		}

		// İlk adımda Token dönmüyoruz, RequiresOtp durumunu işaretliyoruz
		return new LoginResponseDto(Token: null, RequiresOtp: true, Email: kullanici.Email);
	}
}