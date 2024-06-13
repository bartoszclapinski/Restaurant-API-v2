using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Category { get; set; } = default!;
	public bool HasDelivery { get; set; }
	public string? City { get; set; }
	public string? Street { get; set; }
	public string? PostalCode { get; set; }
	
	public List<DishDto> Dishes { get; set; } = [];
	
	public static RestaurantDto FromEntity(Restaurant restaurant)
	{
		return new RestaurantDto
		{
			Name = restaurant.Name,
			Description = restaurant.Description,
			Category = restaurant.Category,
			HasDelivery = restaurant.HasDelivery,
			City = restaurant.Address?.City,
			Street = restaurant.Address?.Street,
			PostalCode = restaurant.Address?.PostalCode,
			Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()
		};
	}
}