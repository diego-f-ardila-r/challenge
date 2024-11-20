// UnauthorizedExceptionHandler.cs
using Metafar.Challenge.Infrastructure.Constants;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.Infrastructure.Handlers;

/// <summary>
/// Handles unauthorized exceptions and sets the appropriate HTTP response.
/// </summary>
public class UnauthorizedExceptionHandler(ILogger<UnauthorizedExceptionHandler> logger) : BaseExceptionHandler<UnauthorizedException>(logger)
{
    protected override void SetErrorResponse(ResponseModel<object>? responseResult, UnauthorizedException exception)
    {
        responseResult?.SetUnauthorizedErrorResponse(exception.Message);
    }
    protected override int GetStatusCode()
    {
        return StatusCodes.Status401Unauthorized;
    }
}
