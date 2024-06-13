using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantRepository restaurantRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
	public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
	{
		logger.LogInformation("Getting all restaurants");
		var restaurants = await restaurantRepository.GetAllAsync();
		return restaurants.Select(RestaurantDto.FromEntity);
	}

	public async Task<RestaurantDto?> GetRestaurantByIdAsync(string id)
	{
		logger.LogInformation("Getting restaurant by id {Id}", id);
		var guidId = IsGuidValid(id);
		if (guidId is null) return null;
		Restaurant? restaurant = await restaurantRepository.GetByIdAsync(guidId.Value);
		return RestaurantDto.FromEntity(restaurant!);
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
