using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(
	ILogger<CreateRestaurantCommandHandler> logger,
	IMapper mapper,
	IRestaurantRepository restaurantRepository) : IRequestHandler<CreateRestaurantCommand, Guid>
{

	public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating restaurant {@Restaurant}", request);
		var restaurant = mapper.Map<Restaurant>(request);
		return await restaurantRepository.CreateAsync(restaurant);
	}
}