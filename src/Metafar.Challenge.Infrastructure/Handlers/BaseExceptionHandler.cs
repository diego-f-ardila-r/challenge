// BaseExceptionHandler.cs
using Metafar.Challenge.Infrastructure.Constants;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.Infrastructure.Handlers;

/// <summary>
/// Base class for exception handlers.
/// </summary>
public abstract class BaseExceptionHandler<TException>(ILogger logger) : IExceptionHandler where TException : System.Exception
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not TException specificException)
        {
            return false;
        }

        logger.LogError(exception, "Application Exception occurred: {Message}", exception.Message);

        var error = new {
            Message = exception.Message,
            StackTrace = exception.StackTrace
        };

        var responseResult = (ResponseModel<object>?)httpContext?.RequestServices?.GetService(typeof(ResponseModel<object>));

        SetErrorResponse(responseResult, specificException);

        httpContext.Response.StatusCode = GetStatusCode();
        httpContext.Response.ContentType = HttpHeaderConstant.ApplicationJsonContentType;

        await httpContext.Response.WriteAsync(JsonSerializerUtility.SetObjectPropertiesToCamelCase(responseResult), cancellationToken);

        return true;
    }

    protected abstract void SetErrorResponse(ResponseModel<object>? responseResult, TException exception);

    protected abstract int GetStatusCode();
}