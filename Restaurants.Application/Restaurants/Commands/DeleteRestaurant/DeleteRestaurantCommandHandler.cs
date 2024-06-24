using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
	ILogger<DeleteRestaurantCommandHandler> logger,
	IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{

	public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Deleting restaurant by id {RestaurantId}", request.RestaurantId);
		Restaurant? restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
		if (restaurant is null) return false;
		await restaurantRepository.DeleteAsync(restaurant);
		return true;
	}
}