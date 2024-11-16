using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.Infrastructure.Handlers;

/// <summary>
/// Handles global exceptions and logs them.
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        // Validate exception type
        if (exception is not { } ex)
        {
            return false;
        }

        logger.LogError(
            exception, "Application Exception occurred: {Message}", exception.Message);

        var error = new {
            Message = exception.Message,
            StackTrace = exception.StackTrace
        };

        // Get instance to retrive the response model
        var responseResult = (ResponseModel<object>?)httpContext?.RequestServices?.GetService(typeof(ResponseModel<object>));

        // Add error details
        responseResult?.SetInternalErrorServerResponse(error);

        // Set status code
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        // Set headers
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(JsonSerializerUtility.SetObjectPropertiesToCamelCase(responseResult), cancellationToken);

        return true;
    }
}