using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishForRestaurantCommand(Guid restaurantId, Guid dishId) : IRequest
{
	public Guid RestaurantId { get; } = restaurantId;
	public Guid DishId { get; } = dishId;
}