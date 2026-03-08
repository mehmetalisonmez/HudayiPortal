using HudayiPortal.Domain.Entities;

namespace HudayiPortal.Application.Interfaces;

public interface IJwtTokenGenerator
{
	string GenerateToken(Kullanici kullanici, string rolAdi);
}