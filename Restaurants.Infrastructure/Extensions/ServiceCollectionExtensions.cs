using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<RestaurantDbContext>(
				o => o.UseSqlServer(configuration.GetConnectionString("RestaurantsDb")));

		services.AddIdentityApiEndpoints<User>()
			.AddRoles<IdentityRole>()
			.AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
			.AddEntityFrameworkStores<RestaurantDbContext>();
		
		services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
		services.AddScoped<IRestaurantRepository, RestaurantRepository>();
		services.AddScoped<IDishRepository, DishRepository>();
		services.AddAuthorizationBuilder()
			.AddPolicy(PolicyNames.HasNationality, p => p.RequireClaim(AppClaimTypes.Nationality, "Polish"))
			.AddPolicy(PolicyNames.AtLeast20, p => p.AddRequirements(new MinimumAgeRequirement(20)));
		
		services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementsHandler>();
	}
}