using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController(IRestaurantsService restaurantsService) : ControllerBase
{
	
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		return Ok(await restaurantsService.GetAllRestaurantsAsync());
	}
	
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		Restaurant? restaurant = await restaurantsService.GetRestaurantByIdAsync(id);
		
		if (restaurant is null) return NotFound();
		return Ok(restaurant);
	}
}