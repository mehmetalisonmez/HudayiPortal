namespace HudayiPortal.Application.Interfaces;

public interface IEmailService
{
	Task SendVerificationEmailAsync(string toEmail, string verificationLink);
    Task SendOtpEmailAsync(string toEmail, string code);
}


