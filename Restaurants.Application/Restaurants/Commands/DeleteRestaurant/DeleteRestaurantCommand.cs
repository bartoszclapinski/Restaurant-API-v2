using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(Guid id) : IRequest<bool>
{
	public Guid RestaurantId { get; } = id;
}