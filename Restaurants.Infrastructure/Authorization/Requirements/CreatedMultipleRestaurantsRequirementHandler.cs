using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreatedMultipleRestaurantsRequirementHandler(
	IRestaurantRepository restaurantRepository,
	IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{

	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
	{
		CurrentUser currentUser = userContext.GetCurrentUser();
		var restaurants = await restaurantRepository.GetAllAsync();
		var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser.Id);
		if (userRestaurantsCreated > requirement.MinimumRestaurantsCreated)
		{
			context.Succeed(requirement);
		}
		else
		{
			context.Fail();
		}
	}
}