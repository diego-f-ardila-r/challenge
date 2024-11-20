using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Queries.Card;

public class CardQueryRepository(MetafarDbContext context) : EntityFrameworkBaseRepository<CardEntity>(context), ICardQueryRepository
{
    /// <summary>
    /// Retrieves a card by its card number
    /// </summary>
    /// <param name="cardNumber">The card number to search for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the card model if found; otherwise, null.</returns>
    /// <remarks>Access is through SQL Server.</remarks>
    public async Task<CardEntity?> GetCardByNumberAsync(int cardNumber)
    {
        return await context.Set<CardEntity>().FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
    }
}