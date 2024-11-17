using MediatR;
using Metafar.Challenge.UseCase.Account.Queries.GetAccountInformationByCardNumber;
using Microsoft.AspNetCore.Mvc;

namespace Metafar.Challenge.WebApi.Controllers.v1;

/// <summary>
/// Represents a controller for managing accounts in the API.
/// </summary>
/// [ApiController]
[Route("api/v1/[controller]s")]
public class AccountController(IMediator mediator, GetAccountInfoByCardNumberQuery getAccountInfoBayCardQuery) : Controller
{
    /// <summary>
    /// User Login
    /// </summary>
    //[Authorize]
    [HttpGet("{cardNumber}")]
    public async Task<IActionResult> GetAccountByCard(int cardNumber)
    {
        getAccountInfoBayCardQuery.CardNumber = cardNumber;
        var response = await mediator.Send(getAccountInfoBayCardQuery);
        return Ok(response);
    }
}