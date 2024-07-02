﻿using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
	public CurrentUser GetCurrentUser()
	{
		ClaimsPrincipal user = httpContextAccessor.HttpContext?.User 
		                       ?? throw new InvalidOperationException("User context is not available");
		
		if (user.Identity is not { IsAuthenticated: true })
			throw new InvalidOperationException("User is not authenticated");
		
		var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
		var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
		var roles = user.FindAll(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

		return new CurrentUser(userId, email, roles);
	}
}