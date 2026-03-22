using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Auth.Queries.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IJwtTokenGenerator _jwtTokenGenerator;

	public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
	{
		_unitOfWork = unitOfWork;
		_jwtTokenGenerator = jwtTokenGenerator;
	}

	public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.Email == request.Email && k.SilindiMi != true)
			.Include(k => k.Rol)
			.FirstOrDefaultAsync(cancellationToken);

		if (kullanici is null)
			throw new UnauthorizedAccessException("E-posta veya þifre hatalý.");

		if (string.IsNullOrEmpty(kullanici.SifreHash))
			throw new UnauthorizedAccessException("Bu hesap için þifre tanýmlanmamýþ.");

		var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Sifre, kullanici.SifreHash);

		if (!isPasswordValid)
			throw new UnauthorizedAccessException("E-posta veya þifre hatalý.");

 		var token = _jwtTokenGenerator.GenerateToken(kullanici, kullanici.RolId.ToString());

		return new LoginResponseDto(token);
	}
}