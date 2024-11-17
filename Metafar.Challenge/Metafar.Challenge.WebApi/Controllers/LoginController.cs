using MediatR;
using Metafar.Challenge.UseCase.Security.Queries.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Metafar.Challenge.WebApi.Controllers;

/// <summary>
/// Represents a controller for managing roles in the API.
/// </summary>
/// [ApiController]
[Route("api/v1/[controller]")]
public class LoginController(IMediator mediator, AuthenticationQuery authenticationQuery) : Controller
{
    /// <summary>
    /// User Login
    /// </summary>
    //[Authorize]
    [HttpGet("{{cardNumber}}/{{pin}}")]
    public async Task<IActionResult> LoginAsync(int cardNumber, int pin)
    {
        authenticationQuery.CardNumber = cardNumber;
        authenticationQuery.Pin = pin;
        var response = await mediator.Send(authenticationQuery);
        return Ok(response);
    }
}