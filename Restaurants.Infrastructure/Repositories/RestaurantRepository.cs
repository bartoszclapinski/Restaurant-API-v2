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
	
	public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase)
	{
		if (searchPhrase is null) return await GetAllAsync();
		
		IEnumerable<Restaurant> restaurants = await context.Restaurants
			.Where(r => r.Name.Contains(searchPhrase) || r.Description.Contains(searchPhrase))
			.ToListAsync();
		return restaurants;
	}

	public async Task<Restaurant?> GetByIdAsync(Guid id)
	{
		Restaurant? restaurant = await context.Restaurants
			.Include(r => r.Dishes)
			.FirstOrDefaultAsync(r => r.RestaurantId == id);
		return restaurant;
	}

	public async Task<Guid> CreateAsync(Restaurant restaurant)
	{
		context.Restaurants.Add(restaurant);
		await context.SaveChangesAsync();
		return restaurant.RestaurantId;
	}

	public async Task DeleteAsync(Restaurant restaurant)
	{
		context.Remove(restaurant);
		await context.SaveChangesAsync();
	}

	public Task SaveChangesAsync() => context.SaveChangesAsync();

}