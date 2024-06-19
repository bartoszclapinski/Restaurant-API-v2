using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddApplication(this IServiceCollection services)
	{
		Assembly applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
		
		services.AddAutoMapper(applicationAssembly);
		services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
	}
}