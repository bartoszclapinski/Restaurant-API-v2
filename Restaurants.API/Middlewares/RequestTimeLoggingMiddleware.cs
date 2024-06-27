using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		var stopwatch = Stopwatch.StartNew();
		await next.Invoke(context);
		stopwatch.Stop();
		
		//	Log the request time only if it's greater than 4 seconds
		if (stopwatch.ElapsedMilliseconds > 4000)
		{
			logger.LogInformation("Request [{Verb}] at {Path} took {ElapsedMilliseconds} ms",
				context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
		}
	}
}