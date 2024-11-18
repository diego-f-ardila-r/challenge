using MediatR;
using Microsoft.AspNetCore.Mvc;
using Metafar.Challenge.UseCase.Operation.Queries;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Model;

namespace Metafar.Challenge.WebApi.Controllers.v1;

[ApiController]
[Route("v1/[controller]s")]
public class OperationController(
    IMediator mediator,
    GetOperationsByCardNumberQuery getOperationsByCardNumberQuery
    ) : ControllerBase
{
    /// <summary>
    /// Gets operations by card number with pagination.
    /// </summary>
    /// <param name="cardNumber">The card number to get operations for.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The page size for pagination.</param>
    [HttpGet("{cardNumber}")]
    public async Task<IActionResult> GetOperationsByCardNumberPaginatedAsync(int cardNumber, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        getOperationsByCardNumberQuery.CardNumber = cardNumber;
        getOperationsByCardNumberQuery.PageNumber = pageNumber;
        getOperationsByCardNumberQuery.PageSize = pageSize;
      
        var response = await mediator.Send(getOperationsByCardNumberQuery);
        return Ok(response);
    }
}