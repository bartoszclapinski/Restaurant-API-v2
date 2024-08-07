﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
	
	[HttpGet]
	[AllowAnonymous]
	//[Authorize(Policy = PolicyNames.CreatedAtLeast2Restaurants)]
	//[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
	public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery]GetAllRestaurantsQuery query)
	{
		return Ok(await mediator.Send(query));
	}
	
	[HttpGet("{id}")]
	[AllowAnonymous]
	//[Authorize(Policy = PolicyNames.HasNationality)]
	public async Task<ActionResult<RestaurantDto>> GetById(string id)
	{
		
		if (!Guid.TryParse(id, out Guid restaurantId)) return BadRequest();
		RestaurantDto restaurant = await mediator.Send(new GetRestaurantByIdQuery(restaurantId));
		
		return Ok(restaurant);
	}

	[HttpPost]
	[Authorize(Roles = UserRoles.Owner)]
	public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
	{
		Guid restaurantId = await mediator.Send(command);
		return CreatedAtAction(nameof(GetById), new { id = restaurantId }, null);
	}
	
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteRestaurant(string id)
	{
		if (!Guid.TryParse(id, out Guid restaurantId)) return BadRequest();
		await mediator.Send(new DeleteRestaurantCommand(restaurantId));
		
		return NoContent();
	}

	[HttpPatch("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateRestaurant(string id, UpdateRestaurantCommand command)
	{
		if (!Guid.TryParse(id, out Guid guidId)) return BadRequest();
		command.RestaurantId = guidId;
		await mediator.Send(command);
		
		return NoContent();
	}
}