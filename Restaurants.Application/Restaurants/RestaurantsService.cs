using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantRepository restaurantRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
	public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
	{
		logger.LogInformation("Getting all restaurants");
		return await restaurantRepository.GetAllAsync();
	}

	public async Task<Restaurant?> GetRestaurantByIdAsync(string id)
	{
		logger.LogInformation("Getting restaurant by id {Id}", id);
		var guidId = IsGuidValid(id);
		if (guidId is null) return null;
		return await restaurantRepository.GetByIdAsync(guidId.Value);
	}
	
	private Guid? IsGuidValid(string id)
	{
		if (!Guid.TryParse(id, out Guid guidId))
		{
			return null;
		}
		return guidId;
	}
}
