using HudayiPortal.Application.Interfaces;
using HudayiPortal.Application.Settings;
using HudayiPortal.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HudayiPortal.Infrastructure.Services;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly JwtSettings _jwtSettings;

	public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
	{
		_jwtSettings = jwtSettings.Value;
	}

	public string GenerateToken(Kullanici kullanici, string rolAdi)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, kullanici.Id.ToString()),
			new(JwtRegisteredClaimNames.Email, kullanici.Email ?? string.Empty),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new(ClaimTypes.NameIdentifier, kullanici.Id.ToString()),
			new(ClaimTypes.Name, $"{kullanici.Ad} {kullanici.Soyad}"),
			new(ClaimTypes.Role, rolAdi)
		};

		var token = new JwtSecurityToken(
			issuer: _jwtSettings.Issuer,
			audience: _jwtSettings.Audience,
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
			signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}