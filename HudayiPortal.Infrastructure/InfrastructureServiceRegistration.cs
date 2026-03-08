using HudayiPortal.Application.Interfaces;
using HudayiPortal.Application.Settings;
using HudayiPortal.Domain.Repositories;
using HudayiPortal.Infrastructure.Persistence;
using HudayiPortal.Infrastructure.Repositories;
using HudayiPortal.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

		return services;
	}
}