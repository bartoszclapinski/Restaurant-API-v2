﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
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

		services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
		services.AddScoped<IRestaurantRepository, RestaurantRepository>();
		services.AddScoped<IDishRepository, DishRepository>();
	}
}