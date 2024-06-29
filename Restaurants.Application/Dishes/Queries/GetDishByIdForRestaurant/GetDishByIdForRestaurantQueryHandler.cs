using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(
	ILogger<GetDishByIdForRestaurantQueryHandler> logger,
	IMapper mapper,
	IRestaurantRepository restaurantRepository) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{

	public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting dish for restaurant: {@RestaurantId} and dish: {@DishId}", request.RestaurantId, request.DishId);
		Restaurant restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId) 
		                        ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
		Dish dish = restaurant.Dishes.FirstOrDefault(d => d.DishId == request.DishId) 
		            ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());
		var dishDto = mapper.Map<DishDto>(dish);
		
		return dishDto;
	}
}