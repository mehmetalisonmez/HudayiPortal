namespace HudayiPortal.Application.Settings;

public sealed class JwtSettings
{
	public const string SectionName = "JwtSettings";

	public string SecretKey { get; set; } = null!;
	public string Issuer { get; set; } = null!;
	public string Audience { get; set; } = null!;
	public int ExpiryMinutes { get; set; }
}