using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
	Task<Guid> CreateAsync(Dish entity);
}