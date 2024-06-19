using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(
	ILogger<GetAllRestaurantsQueryHandler> logger,
	IMapper mapper,
	IRestaurantRepository restaurantRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{

	public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting restaurant by id {Id}", request.RestaurantId);
		if (!Guid.TryParse(request.RestaurantId.ToString(), out _)) return null;
		Restaurant? restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
		return mapper.Map<RestaurantDto?>(restaurant);
	}
}