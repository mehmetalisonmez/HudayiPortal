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
}
