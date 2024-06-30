using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class DishRepository(RestaurantDbContext context) : IDishRepository
{
	
	public async Task<Guid> CreateAsync(Dish entity)
	{
		context.Dishes.Add(entity);
		await context.SaveChangesAsync();
		return entity.DishId;
	}
	
	public async Task DeleteDishAsync(Dish entity)
	{
		context.Remove(entity);
		await context.SaveChangesAsync();
	}
}