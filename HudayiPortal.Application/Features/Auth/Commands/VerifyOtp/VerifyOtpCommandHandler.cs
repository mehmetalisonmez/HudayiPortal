using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Features.Auth.Queries.Login;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HudayiPortal.Application.Features.Auth.Commands.VerifyOtp;

public sealed class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, LoginResponseDto>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IMemoryCache _memoryCache;

	public VerifyOtpCommandHandler(
		IUnitOfWork unitOfWork,
		IJwtTokenGenerator jwtTokenGenerator,
		IMemoryCache memoryCache)
	{
		_unitOfWork = unitOfWork;
		_jwtTokenGenerator = jwtTokenGenerator;
		_memoryCache = memoryCache;
	}

	public async Task<LoginResponseDto> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
	{
		var cacheKey = $"OTP_{request.Email}";
		
		if (!_memoryCache.TryGetValue(cacheKey, out string? cachedOtp))
		{
			throw new BusinessException("Doğrulama kodunun süresi dolmuş veya kod hiç gönderilmemiş.");
		}

		if (cachedOtp != request.OtpCode)
		{
			throw new BusinessException("Girdiğiniz doğrulama kodu hatalı.");
		}

		// Doğrulama başarılı – kod tek seferliktir, bellekten temizleyelim
		_memoryCache.Remove(cacheKey);

		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.Email == request.Email && k.SilindiMi != true)
			.Include(k => k.Rol)
			.FirstOrDefaultAsync(cancellationToken);

		if (kullanici is null)
		{
			throw new BusinessException("Kullanıcı bulunamadı.");
		}

		// RolId'yi Controller'ların beklediği Claim metinlerine çeviriyoruz
		string roleName = kullanici.RolId switch
		{
			1 => "Öğrenci",
			2 => "Admin",
			3 => "Personel",
			_ => "Öğrenci"
		};

		// Başarılı giriş: JWT Token üretiyoruz
		var token = _jwtTokenGenerator.GenerateToken(kullanici, roleName);

		return new LoginResponseDto(Token: token, RequiresOtp: false, Email: kullanici.Email);
	}
}