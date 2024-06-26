using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next.Invoke(context);
		}
		catch (Exception e)
		{
			logger.LogError(e, e.Message);
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsync("Something went wrong.");
		}
	}
}