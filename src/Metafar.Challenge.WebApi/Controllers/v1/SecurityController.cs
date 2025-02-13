using MediatR;
using Metafar.Challenge.UseCase.Security.Queries.SignInUserByCard;
using Microsoft.AspNetCore.Mvc;

namespace Metafar.Challenge.WebApi.Controllers.v1;

/// <summary>
/// Represents a controller for the APP's Security.
/// </summary>
/// [ApiController]
[Route("v1/[controller]")]
public class SecurityController(IMediator mediator, SignInUserByCardQuery signInUserByCardQuery) : Controller
{
    /// <summary>
    /// Get a token by card number and pin.
    /// </summary>
    [HttpGet("{cardNumber}/{pin}")]
    public async Task<IActionResult> SignInAsync(int cardNumber, int pin)
    {
        signInUserByCardQuery.CardNumber = cardNumber;
        signInUserByCardQuery.Pin = pin;
        var response = await mediator.Send(signInUserByCardQuery);
        return Ok(response);
    }
}