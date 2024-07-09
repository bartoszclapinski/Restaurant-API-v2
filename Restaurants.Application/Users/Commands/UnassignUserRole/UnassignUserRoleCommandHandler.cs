using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommandHandler(
	ILogger<UnassignUserRoleCommandHandler> logger, 
	UserManager<User> userManager,
	RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
{

	public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Unassigning user role: {@Request}", request);
		User user = await userManager.FindByEmailAsync(request.Email)
		            ?? throw new NotFoundException(nameof(User), request.Email);
		
		IdentityRole role = await roleManager.FindByNameAsync(request.UserRole)
		                    ?? throw new NotFoundException(nameof(IdentityRole), request.UserRole);
		
		await userManager.RemoveFromRoleAsync(user, role.Name!);
	}
}