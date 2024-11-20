// GlobalExceptionHandler.cs
using Metafar.Challenge.Infrastructure.Constants;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.Infrastructure.Handlers;

/// <summary>
/// Handles global exceptions and logs them.
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : BaseExceptionHandler<System.Exception>(logger)
{
    protected override void SetErrorResponse(ResponseModel<object>? responseResult, System.Exception exception)
    {
        var error = new {
            Message = exception.Message,
            StackTrace = exception.StackTrace
        };

        responseResult?.SetInternalErrorServerResponse(error);
    }

    protected override int GetStatusCode()
    {
        return StatusCodes.Status500InternalServerError;
    }
}