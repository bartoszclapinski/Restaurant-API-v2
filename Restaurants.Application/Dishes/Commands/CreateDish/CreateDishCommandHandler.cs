using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(
	ILogger<CreateDishCommandHandler> logger,
	IRestaurantRepository restaurantRepository,
	IDishRepository dishRepository,
	IMapper mapper) : IRequestHandler<CreateDishCommand, Guid>
{

	public async Task<Guid> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating dish: {@DishRequest}", request);
		var restaurant = restaurantRepository.GetByIdAsync(request.RestaurantId)
			?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

		var dish = mapper.Map<Dish>(request);
		return await dishRepository.CreateAsync(dish);
	}
}