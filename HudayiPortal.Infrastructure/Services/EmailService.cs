using FluentEmail.Core;
using HudayiPortal.Application.Interfaces;

namespace HudayiPortal.Infrastructure.Services;

public sealed class EmailService : IEmailService
{
	private readonly IFluentEmailFactory _emailFactory;

	public EmailService(IFluentEmailFactory emailFactory)
	{
		_emailFactory = emailFactory;
	}

	public async Task SendVerificationEmailAsync(string toEmail, string verificationLink)
	{
		var email = _emailFactory.Create()
			.To(toEmail)
			.Subject("HudayiPortal - Email Doğrulama")
			.Body($"Lütfen email adresinizi doğrulamak için bağlantıya tıklayın: {verificationLink}");

		await email.SendAsync();
	}

	public async Task SendOtpEmailAsync(string toEmail, string code)
	{
		var email = _emailFactory.Create()
			.To(toEmail)
			.Subject("HudayiPortal - Giriş Doğrulama Kodu (OTP)")
			.Body($"Giriş doğrulaması için tek seferlik güvenlik kodunuz: {code}\nBu kod 3 dakika boyunca geçerlidir. Kodunuzu kimseyle paylaşmayınız.");

		await email.SendAsync();
	}
}
