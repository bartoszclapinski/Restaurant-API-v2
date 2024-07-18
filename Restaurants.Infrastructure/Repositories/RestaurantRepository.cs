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
	
	public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize)
	{
		var searchPhraseToLower = searchPhrase?.ToLower();
		
		var baseQuery = context.Restaurants
			.Where(r => searchPhraseToLower == null 
			            || r.Name.Contains(searchPhraseToLower) || r.Description.Contains(searchPhraseToLower));
		
		var totalCount = await baseQuery.CountAsync();
		
		var restaurants = await baseQuery
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();
		
		return (restaurants, totalCount);
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