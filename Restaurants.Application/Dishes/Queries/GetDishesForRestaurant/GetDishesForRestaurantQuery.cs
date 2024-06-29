using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery(Guid restaurantId) : IRequest<IEnumerable<DishDto>>
{
	public Guid RestaurantId { get; } = restaurantId;
}