using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

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
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesForRestaurant([FromRoute] string restaurantId)
	{
		if (!Guid.TryParse(restaurantId, out Guid id)) return BadRequest();
		var dishes = await mediator.Send(new GetDishesForRestaurantQuery(id));
		return Ok(dishes);
	}
	
	[HttpGet("{dishId}")]
	public async Task<ActionResult<DishDto>> GetDishForRestaurant([FromRoute] string restaurantId, [FromRoute] string dishId)
	{
		if (!Guid.TryParse(restaurantId, out Guid restaurantGuid) || !Guid.TryParse(dishId, out Guid dishGuid)) return BadRequest();
		DishDto dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantGuid, dishGuid));
		return Ok(dish);
	}
}