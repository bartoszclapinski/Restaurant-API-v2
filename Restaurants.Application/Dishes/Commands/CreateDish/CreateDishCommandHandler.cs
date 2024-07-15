using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(
	ILogger<CreateDishCommandHandler> logger,
	IRestaurantRepository restaurantRepository,
	IDishRepository dishRepository,
	IMapper mapper,
	IRestaurantAuthorizationService ras) : IRequestHandler<CreateDishCommand, Guid>
{

	public async Task<Guid> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating dish: {@DishRequest}", request);
		Restaurant restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId)
			?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
		
		if (!ras.Authorize(restaurant, ResourceOperation.Update)) throw new ForbidException();
		
		var dish = mapper.Map<Dish>(request);
		return await dishRepository.CreateAsync(dish);
	}
}