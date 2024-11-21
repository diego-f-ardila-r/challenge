using MediatR;
using Metafar.Challenge.UseCase.Account.Commands.WithdrawFromAccount;
using Metafar.Challenge.UseCase.Account.Queries.GetAccountInformationByCardNumber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Metafar.Challenge.WebApi.Controllers.v1;

/// <summary>
/// Represents a controller for managing accounts in the API.
/// </summary>
[Authorize]
[Route("v1/[controller]s")]
public class AccountController(
    IMediator mediator, 
    GetAccountInfoByCardNumberQuery getAccountInfoBayCardQuery
    ) : Controller
{
    /// <summary>
    /// Get account information by card number.
    /// </summary>
    //[Authorize]
    [HttpGet("{cardNumber}")]
    public async Task<IActionResult> GetAccountInfoByCard(int cardNumber)
    {
        getAccountInfoBayCardQuery.CardNumber = cardNumber;
        var response = await mediator.Send(getAccountInfoBayCardQuery);
        return Ok(response);
    }
    
    /// <summary>
    /// Withdraw from account.
    /// </summary>
    //[Authorize]
    [HttpPost("balance/withdraw")]
    public async Task<IActionResult> WithdrawFromAccount([FromBody] WithdrawFromAccountCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

}