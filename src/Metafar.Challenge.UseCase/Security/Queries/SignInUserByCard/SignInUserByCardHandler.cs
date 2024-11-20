using FluentValidation;
using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Card.Commands;
using Metafar.Challenge.Repository.Queries.Card;
using Metafar.Challenge.UseCase.Constants;
using Microsoft.Extensions.Logging;

namespace Metafar.Challenge.UseCase.Security.Queries.SignInUserByCard;

/// <summary>
/// Handles authentication query, this process validates if the card number and pin are correct,
/// the user has 4 attempts to enter the correct pin, otherwise the card will be blocked.
/// <returns>A task that represents the asynchronous operation with a JWT token.</returns>
/// </summary>
public class SignInUserByCardHandler(
    ResponseModel<TokenDto> response,
    ICardQueryRepository cardQueryRepository,
    ICardCommandRepository cardCommandRepository,
    IValidator<SignInUserByCardQuery> validator,
    JwtTokenUtility jwtTokenUtility,
    ILogger<SignInUserByCardHandler> logger) 
    : IRequestHandler<SignInUserByCardQuery, ResponseModel<TokenDto>>
{
    public async Task<ResponseModel<TokenDto>> Handle(Queries.SignInUserByCard.SignInUserByCardQuery request, CancellationToken cancellationToken)
    {
        // Validate the request
        var resultValidator = await validator.ValidateAsync(request, cancellationToken);
        if (!resultValidator.IsValid) throw new ValidatorException(MessageCodeConstant.ValidationError, resultValidator.Errors);
        
        // Get
        var card = await cardQueryRepository.GetCardByNumberAsync(request.CardNumber);
        
        // Validate if the card was found
        if (card == null) 
            throw new NoContentException(MessageCodeConstant.CardNotFound);

        // Validate if the card is locked
        if (card.IsBlocked) 
            throw new FunctionalException(MessageCodeConstant.CardHasBeenBlocked);
        
        // Validate if the pin is correct
        if (card.AccessPin != request.Pin) 
        {
            // Validate if FailedAttempts is greater than 4, which means the card must be blocked
            if (card.FailedAttempts >= 4)
            {
                await cardCommandRepository.BlockCardAsync(card);
                throw new FunctionalException(MessageCodeConstant.CardHasBeenBlocked);
            }
            
            // Increment the failed attempts on the card
            await cardCommandRepository.IncrementFailedAttemptsAsync(card);
            throw new FunctionalException(MessageCodeConstant.InvalidCardNumberOrPin);
        }
        
        // Reset the failed attempts count to zero
        if (card.FailedAttempts > 0) 
            await cardCommandRepository.ResetFailedAttemptsAsync(card);
        
        // Generate a JWT token
        response.Data = new TokenDto
        {
            Token = jwtTokenUtility.GenerateJwtToken(card.CardId.ToString(), card.CardNumber.ToString())
        };

        return response;
    }
}