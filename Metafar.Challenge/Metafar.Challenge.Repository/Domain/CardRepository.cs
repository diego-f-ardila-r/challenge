using Metafar.Challenge.Model.Entities;
using Metafar.Challenge.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Domain;

public class CardRepository(DomainDbContext context) : CoreRepository<CardModel>(context), ICardRepository
{
    /// <summary>
    /// Retrieves a card by its card number and PIN.
    /// </summary>
    /// <param name="cardNumber">The card number to search for.</param>
    /// <param name="pin">The PIN associated with the card number.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the card model if found; otherwise, null.</returns>
    /// <remarks>Access is through SQL Server.</remarks>
    public async Task<CardModel?> GetCardByCardNumberAndPinAsync(int cardNumber, int pin)
    {
        return await context.Set<CardModel>().FirstOrDefaultAsync(c => c.CardNumber == cardNumber && c.AccessPin == pin);
    }
    
    /// <summary>
    /// Blocks a card by its card number.
    /// </summary>
    /// <param name="cardNumber">The card number to block.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task BlockCardAsync(int cardNumber)
    {
        await context.Cards.Where(c => c.CardNumber == cardNumber)
            .ExecuteUpdateAsync(c => c.SetProperty(b => b.IsBlocked, true));
    }
}