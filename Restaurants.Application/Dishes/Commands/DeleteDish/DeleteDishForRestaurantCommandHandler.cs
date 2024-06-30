using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishForRestaurantCommandHandler(
	ILogger<DeleteDishForRestaurantCommandHandler> logger,
	IRestaurantRepository restaurantRepository,
	IDishRepository dishRepository) : IRequestHandler<DeleteDishForRestaurantCommand>
{

	public async Task Handle(DeleteDishForRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Deleting dish: {@DishRequest}", request);
		
		Restaurant restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId)
		                        ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

		Dish dish = restaurant.Dishes.FirstOrDefault(d => d.DishId == request.DishId)
		            ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());
		
		await dishRepository.DeleteDishAsync(dish);
	}
}