using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(
	ILogger<RestaurantAuthorizationService> logger,
	IUserContext userContext) : IRestaurantAuthorizationService
{
	public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
	{
		CurrentUser currentUser = userContext.GetCurrentUser();
		logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
			currentUser.Email, resourceOperation, restaurant.Name);

		if (resourceOperation is ResourceOperation.Read or ResourceOperation.Create)
		{
			logger.LogInformation("Create/Read operation - authorized");
			return true;
		}

		if (resourceOperation is ResourceOperation.Delete && currentUser.IsInRole(UserRoles.Admin))
		{
			logger.LogInformation("Admin user, delete operation - authorized");
			return true;
		}
		
		if (resourceOperation is ResourceOperation.Delete or ResourceOperation.Update 
		    && restaurant.OwnerId == currentUser.Id)
		{
			logger.LogInformation("Owner user, delete/update operation - authorized");
			return true;
		}

		return false;
	}	
}