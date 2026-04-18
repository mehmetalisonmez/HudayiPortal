using HudayiPortal.Application.Interfaces;
using HudayiPortal.Application.Settings;
using HudayiPortal.Domain.Repositories;
using HudayiPortal.Infrastructure.Persistence;
using HudayiPortal.Infrastructure.Repositories;
using HudayiPortal.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace HudayiPortal.Infrastructure;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection"),
				sqlOptions => sqlOptions.MigrationsAssembly("HudayiPortal.Infrastructure")));

		services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

		var smtpHost = configuration["Smtp:Host"] ?? "localhost";
		var smtpPort = int.TryParse(configuration["Smtp:Port"], out var port) ? port : 25;
		var smtpUser = configuration["Smtp:Username"];
		var smtpPass = configuration["Smtp:Password"];
		var fromEmail = configuration["Smtp:FromEmail"] ?? "noreply@hudayiportal.local";

		var smtpClient = new SmtpClient(smtpHost, smtpPort)
		{
			EnableSsl = bool.TryParse(configuration["Smtp:EnableSsl"], out var enableSsl) && enableSsl,
			UseDefaultCredentials = string.IsNullOrWhiteSpace(smtpUser),
			Credentials = !string.IsNullOrWhiteSpace(smtpUser)
				? new NetworkCredential(smtpUser, smtpPass)
				: CredentialCache.DefaultNetworkCredentials
		};

		services
			.AddFluentEmail(fromEmail)
			.AddSmtpSender(smtpClient);

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
		services.AddScoped<IEmailService, EmailService>();
		services.AddHttpContextAccessor();
		services.AddScoped<ICurrentUserService, CurrentUserService>();
		services.AddScoped<IFileService, LocalFileService>();

		return services;
	}
}