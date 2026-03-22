namespace HudayiPortal.Application.Interfaces;

public interface IEmailService
{
	Task SendVerificationEmailAsync(string toEmail, string verificationLink);
}
