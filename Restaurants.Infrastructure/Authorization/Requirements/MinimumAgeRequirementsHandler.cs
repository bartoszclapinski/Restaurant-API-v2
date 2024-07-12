using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementsHandler(
	ILogger<MinimumAgeRequirementsHandler> logger,
	IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{

	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
	{
		CurrentUser currentUser = userContext.GetCurrentUser();
		
		logger.LogInformation("User: {Email}, Date of birth: {DateOfBirth} - handling MinimumAgeRequirement", currentUser.Email, currentUser.DateOfBirth);

		if (currentUser.DateOfBirth is null)
		{
			logger.LogInformation("Authorization denied, user has no date of birth");
			context.Fail();
			return Task.CompletedTask;
		}
		
		if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
		{
			logger.LogInformation("Authorization granted");
			context.Succeed(requirement);
		}
		else
		{
			context.Fail();
		}
		
		return Task.CompletedTask;
	}
}