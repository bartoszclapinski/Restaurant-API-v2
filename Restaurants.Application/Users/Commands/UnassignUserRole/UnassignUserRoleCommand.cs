using MediatR;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommand : IRequest
{
	public string Email { get; set; } = default!;
	public string UserRole { get; set; } = default!;
}