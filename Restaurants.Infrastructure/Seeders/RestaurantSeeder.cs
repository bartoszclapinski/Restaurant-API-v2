using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantDbContext context) : IRestaurantSeeder
{
	public async Task Seed()
	{
		if (await context.Database.CanConnectAsync())
		{
			if (!context.Restaurants.Any())
			{
				var restaurants = GetRestaurants();
				await context.Restaurants.AddRangeAsync(restaurants);
				await context.SaveChangesAsync();
			}
			
			if (!context.Roles.Any())
			{
				var roles = GetRoles();
				await context.Roles.AddRangeAsync(roles);
				await context.SaveChangesAsync();
			}
		}
	}

	private IEnumerable<Restaurant> GetRestaurants()
	{
		List<Restaurant> restaurants =
		[
			new Restaurant
			{
				Name = "KFC",
				Category = "Fast Food",
				Description =
					"KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
				ContactEmail = "contact@kfc.com",
				HasDelivery = true,
				Dishes =
				[
					new Dish
					{
						Name = "Nashville Hot Chicken",
						Description = "Nashville hot chicken (10 pieces)",
						Price = 10.30M
					},
					new Dish
					{
						Name = "Chicken Nuggets",
						Description = "Chicken nuggets (5 pieces)",
						Price = 5.30M
					}
				],
				Address = new Address
				{
					City = "London",
					Street = "Cork St 5",
					PostalCode = "WC2N 5DU"
				}
			},
			new Restaurant
			{
				Name = "McDonald's",
				Category = "Fast Food",
				Description =
					"McDonald's Corporation is an American fast food company, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States.",
				ContactEmail = "contact@mcdonalds.com",
				HasDelivery = true,
				Address = new Address
				{
					City = "London",
					Street = "Boots 193",
					PostalCode = "W1F 8SR"
				}

			}
		];

		return restaurants;
	}

	private IEnumerable<IdentityRole> GetRoles()
	{
		List<IdentityRole> roles =
		[
			new IdentityRole(UserRoles.User),
			new IdentityRole(UserRoles.Owner),
			new IdentityRole(UserRoles.Admin)
		];

		return roles;
	}
}