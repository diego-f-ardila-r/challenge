using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Queries;

public class CardQueryRepository(RepositoryDbContext context) : EntityFrameworkBaseRepository<CardEntity>(context), ICardQueryRepository
{
    /// <summary>
    /// Retrieves a card by its card number and PIN.
    /// </summary>
    /// <param name="cardNumber">The card number to search for.</param>
    /// <param name="pin">The PIN associated with the card number.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the card model if found; otherwise, null.</returns>
    /// <remarks>Access is through SQL Server.</remarks>
    public async Task<CardEntity?> GetCardByCardNumberAndPinAsync(int cardNumber, int pin)
    {
        return await context.Set<CardEntity>().FirstOrDefaultAsync(c => c.CardNumber == cardNumber && c.AccessPin == pin);
    }
}