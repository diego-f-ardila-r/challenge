using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Commands;
using Metafar.Challenge.Repository.Queries;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.UseCase.Security.Queries.Authentication;

/// <summary>
/// Handles authentication queries, this process validates if the card number and pin are correct,
/// the user has 4 attempts to enter the correct pin, otherwise the card will be blocked.
/// <returns>A task that represents the asynchronous operation with a JWT token.</returns>
/// </summary>
public class AuthenticationHandler(
    ResponseModel<TokenDto> response,
    ICardQueryRepository cardQueryRepository,
    ICardCommandRepository cardCommandRepository,
    ILogger<AuthenticationHandler> logger) 
    : IRequestHandler<AuthenticationQuery, ResponseModel<TokenDto>>
{
    public async Task<ResponseModel<TokenDto>> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
    {
        var card = await cardQueryRepository.GetCardByCardNumberAsync(request.CardNumber);
        
        if (card == null) throw new FunctionalException("INVALID_CARD_OR_PIN");

        // Validate if the card is locked
        if (card.IsBlocked) throw new FunctionalException("CARD_IS_BLOCKED");
        
        // Validate if the pin is correct
        if (card.AccessPin != request.Pin) 
        {
            card.FailedAttempts++;
            
            // Validate if FailedAttempts is greater than 4, which means the card must be blocked
            if (card.FailedAttempts > 4)
            {
                await cardCommandRepository.BlockCardAsync(card.CardNumber);
                throw new FunctionalException("THE_CARD_HAS_BEEN_BLOCKED");
            }
            
            // Increment the failed attempts on the card
            await cardCommandRepository.IncrementFailedAttemptsAsync(card);
            throw new FunctionalException("INVALID_CARD_OR_PIN");
        }
        
        // Generate a JWT token
        response.Data = new TokenDto
        {
            Token = JwtTokenUtility.GenerateJwtToken("1a382c6e-fa61-4fa1-87ac-0f3ed5aca962","METAFAR","CHALLENGE", card.CardNumber.ToString())
        };

        return response;
    }
}