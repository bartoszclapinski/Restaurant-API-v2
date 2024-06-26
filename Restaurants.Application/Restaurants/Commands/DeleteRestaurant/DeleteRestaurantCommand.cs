using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(Guid id) : IRequest
{
	public Guid RestaurantId { get; } = id;
}