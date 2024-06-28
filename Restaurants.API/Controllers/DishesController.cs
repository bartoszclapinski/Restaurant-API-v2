using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
public class DishesController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateDish([FromRoute] string restaurantId, CreateDishCommand command)
	{
		if (!Guid.TryParse(restaurantId, out Guid id)) return BadRequest();
		command.RestaurantId = id;
		await mediator.Send(command);
		return Created();
	}
}