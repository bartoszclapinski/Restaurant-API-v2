using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(
	ILogger<GetAllRestaurantsQueryHandler> logger,
	IMapper mapper,
	IRestaurantRepository restaurantRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{

	public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting restaurant by id {Id}", request.RestaurantId);
		Restaurant restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId)
			?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
		return mapper.Map<RestaurantDto>(restaurant);
	}
}