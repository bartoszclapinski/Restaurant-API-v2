using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantRepository(RestaurantDbContext context) : IRestaurantRepository
{
	public async Task<IEnumerable<Restaurant>> GetAllAsync()
	{
		IEnumerable<Restaurant> restaurants = await context.Restaurants.ToListAsync();
		return restaurants;
	}
}