using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(
	ILogger<GetDishesForRestaurantQueryHandler> logger,
	IRestaurantRepository restaurantRepository,
	IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
	public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting dishes for restaurant: {@RestaurantId}", request.RestaurantId);
		Restaurant restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId)
		                        ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

		var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
		return result;
	}
}