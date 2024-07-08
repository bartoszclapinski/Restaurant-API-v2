using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;
using Restaurants.Application.Users.Commands.AssignUserRole;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
	[HttpPatch("user")]
	[Authorize]
	public async Task<IActionResult> UpdateUser(UpdateUserDetailsCommand command)
	{
		await mediator.Send(command);
		return NoContent();
	}
	
	[HttpPost("userRole")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
	{
		await mediator.Send(command);
		return NoContent();
	}
	
}