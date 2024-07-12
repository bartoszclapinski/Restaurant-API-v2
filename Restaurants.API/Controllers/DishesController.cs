using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateDish([FromRoute] string restaurantId, CreateDishCommand command)
	{
		if (!Guid.TryParse(restaurantId, out Guid id)) return BadRequest();
		command.RestaurantId = id;
		Guid dishId = await mediator.Send(command);
		return CreatedAtAction(nameof(GetDishForRestaurant), new { restaurantId = id, dishId }, null);
	}
	
	[HttpGet]
	[Authorize(Policy = PolicyNames.AtLeast20)]
	public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesForRestaurant([FromRoute] string restaurantId)
	{
		if (!Guid.TryParse(restaurantId, out Guid id)) return BadRequest();
		var dishes = await mediator.Send(new GetDishesForRestaurantQuery(id));
		return Ok(dishes);
	}
	
	[HttpGet("{dishId}")]
	public async Task<ActionResult<DishDto>> GetDishForRestaurant([FromRoute] string restaurantId, [FromRoute] string dishId)
	{
		if (!Guid.TryParse(restaurantId, out Guid restaurantGuid) || !Guid.TryParse(dishId, out Guid dishGuid)) 
			return BadRequest();
		DishDto dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantGuid, dishGuid));
		return Ok(dish);
	}
	
	[HttpDelete("{dishId}")]
	public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] string restaurantId, [FromRoute] string dishId)
	{
		if (!Guid.TryParse(restaurantId, out Guid restaurantGuid) || !Guid.TryParse(dishId, out Guid dishGuid)) 
			return BadRequest();
		await mediator.Send(new DeleteDishForRestaurantCommand(restaurantGuid, dishGuid));
		return NoContent();
	}
}