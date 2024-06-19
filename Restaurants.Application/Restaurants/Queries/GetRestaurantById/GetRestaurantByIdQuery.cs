using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery(Guid restaurantId) : IRequest<RestaurantDto?>
{
	public Guid RestaurantId { get; } = restaurantId;
}