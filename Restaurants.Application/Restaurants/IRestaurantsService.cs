using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantsService
{
	Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
	Task<RestaurantDto?> GetRestaurantByIdAsync(string id);
}