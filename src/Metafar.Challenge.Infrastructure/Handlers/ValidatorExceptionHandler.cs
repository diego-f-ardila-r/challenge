// FunctionalExceptionHandler.cs
using Metafar.Challenge.Infrastructure.Constants;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.Infrastructure.Handlers;

/// <summary>
/// Handles functional exceptions and sets the appropriate HTTP response.
/// </summary>
public class ValidatorExceptionHandler(ILogger<ValidatorExceptionHandler> logger) : BaseExceptionHandler<ValidatorException>(logger)
{
    protected override void SetErrorResponse(ResponseModel<object>? responseResult, ValidatorException exception)
    {
        responseResult?.SetValidatorResponse(exception.Message, exception.Data["Errors"]);
    }
    
    protected override int GetStatusCode()
    {
        return StatusCodes.Status400BadRequest;
    }
}