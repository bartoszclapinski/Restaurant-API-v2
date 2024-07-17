using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
	Task<IEnumerable<Restaurant>> GetAllAsync();
	Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);
	Task<Restaurant?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(Restaurant restaurant);
	Task DeleteAsync(Restaurant restaurant);
	Task SaveChangesAsync();
}