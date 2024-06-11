using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

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
}