using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
	ILogger<UpdateRestaurantCommandHandler> logger,
	IMapper mapper,
	IRestaurantRepository restaurantRepository,
	IRestaurantAuthorizationService ras) : IRequestHandler<UpdateRestaurantCommand>
{

	public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating restaurant by id {RestaurantId}", request.RestaurantId);
		Restaurant? restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
		if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
		if (!ras.Authorize(restaurant, ResourceOperation.Update)) throw new ForbidException();
		mapper.Map(request, restaurant);
		
		await restaurantRepository.SaveChangesAsync();
	}
}