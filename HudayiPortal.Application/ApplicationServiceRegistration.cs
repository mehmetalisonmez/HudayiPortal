using FluentValidation;
using HudayiPortal.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HudayiPortal.Application;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		var assembly = Assembly.GetExecutingAssembly();

		services.AddMediatR(cfg =>
			cfg.RegisterServicesFromAssembly(assembly));

		services.AddAutoMapper(assembly);

		services.AddValidatorsFromAssembly(assembly);

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

		return services;
	}
}