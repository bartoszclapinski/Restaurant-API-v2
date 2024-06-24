﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
	
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		return Ok(await mediator.Send(new GetAllRestaurantsQuery()));
	}
	
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		if (!Guid.TryParse(id, out Guid restaurantId)) return BadRequest();
		RestaurantDto? restaurant = await mediator.Send(new GetRestaurantByIdQuery(restaurantId));
		if (restaurant is null) return NotFound();
		return Ok(restaurant);
	}

	[HttpPost]
	public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
	{
		Guid restaurantId = await mediator.Send(command);
		return CreatedAtAction(nameof(GetById), new { id = restaurantId }, null);
	}
	
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteRestaurant(string id)
	{
		if (!Guid.TryParse(id, out Guid restaurantId)) return BadRequest();
		var deleted = await mediator.Send(new DeleteRestaurantCommand(restaurantId));
		if (!deleted) return NotFound();
		return NoContent();
	}

	[HttpPatch("{id}")]
	public async Task<IActionResult> UpdateRestaurant(string id, UpdateRestaurantCommand command)
	{
		if (!Guid.TryParse(id, out Guid guidId)) return BadRequest();
		command.RestaurantId = guidId;
		var isUpdated = await mediator.Send(command);
		if (!isUpdated) return NotFound();
		return NoContent();
	}
}