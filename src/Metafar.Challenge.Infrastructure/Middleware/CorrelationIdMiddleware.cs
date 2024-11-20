using Metafar.Challenge.Infrastructure.Constants;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Metafar.Challenge.Infrastructure.Middleware;

/// <summary>
/// Middleware to handle correlation ID for incoming HTTP requests and responses.
/// </summary>
public class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, CorrelationIdGeneratorUtility correlationIdGenerator)
    {
        var correlationId = GetCorrelationId(context, correlationIdGenerator);
        AddCorrelationIdHeaderToResponse(context, correlationId);
        await next(context);
    }

/// <summary>
/// Retrieves the correlation ID from the HTTP request headers or generates a new one if not present.
/// </summary>
/// <param name="context">The HTTP context of the current request.</param>
/// <param name="correlationIdGenerator">The utility to generate or retrieve the correlation ID.</param>
/// <returns>The correlation ID as a <see cref="StringValues"/>.</returns>
private static StringValues GetCorrelationId(HttpContext context, CorrelationIdGeneratorUtility correlationIdGenerator)
{
    if(context.Request.Headers.TryGetValue(HttpHeaderConstant.CorrelationId, out var correlationId))
    {
        correlationIdGenerator.Set(correlationId);
        return correlationId;
    }
    else
    {
        return correlationIdGenerator.Get();
    }
}

/// <summary>
/// Adds the correlation ID header to the HTTP response.
/// </summary>
/// <param name="context">The HTTP context of the current request.</param>
/// <param name="correlationId">The correlation ID to be added to the response headers.</param>
private static void AddCorrelationIdHeaderToResponse(HttpContext context, StringValues correlationId)
{ 
    context.Response.OnStarting(() =>
    {
        context.Response.Headers.Append(HttpHeaderConstant.CorrelationId, new[] { correlationId.ToString() });
        return Task.CompletedTask;
    });
}
}
