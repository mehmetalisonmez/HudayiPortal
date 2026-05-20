using System;
using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HudayiPortal.Application.Features.Auth.Queries.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMemoryCache _memoryCache;
	private readonly IEmailService _emailService;

	public LoginQueryHandler(
		IUnitOfWork unitOfWork,
		IMemoryCache memoryCache,
		IEmailService emailService)
	{
		_unitOfWork = unitOfWork;
		_memoryCache = memoryCache;
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
		
		// Cache üzerinde 3 dakika saklayalım
		var cacheKey = $"OTP_{kullanici.Email}";
		_memoryCache.Set(cacheKey, otpCode, TimeSpan.FromMinutes(3));

		// E-posta gönderelim
		if (!string.IsNullOrEmpty(kullanici.Email))
		{
			await _emailService.SendOtpEmailAsync(kullanici.Email, otpCode);
		}

		// İlk adımda Token dönmüyoruz, RequiresOtp durumunu işaretliyoruz
		return new LoginResponseDto(Token: null, RequiresOtp: true, Email: kullanici.Email);
	}
}