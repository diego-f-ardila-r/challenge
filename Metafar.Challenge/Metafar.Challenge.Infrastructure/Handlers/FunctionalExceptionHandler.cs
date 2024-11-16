using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.Infrastructure.Handlers;

/// <summary>
/// Handles functional exceptions and sets the appropriate HTTP response.
/// </summary>
public class FunctionalExceptionHandler(ILogger<FunctionalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        // Validate exception type
        if (exception is not FunctionalException functionalException)
        {
            return false;
        }
                        
        // Get instance to retrieve the response model
        var responseResult = (ResponseModel<object>?)httpContext?.RequestServices?.GetService(typeof(ResponseModel<object>));

        // Set message
        responseResult?.SetFunctionalErrorResponse(exception.Message);

        // Set status code
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        // Set headers
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(JsonSerializerUtility.SetObjectPropertiesToCamelCase(responseResult), cancellationToken);

        return true;
    }
}