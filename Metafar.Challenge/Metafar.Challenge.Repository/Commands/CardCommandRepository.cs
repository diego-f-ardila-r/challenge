using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Commands;

public class CardCommandRepository(MetafarDbContext context) : EntityFrameworkBaseRepository<CardEntity>(context), ICardCommandRepository
{
    /// <summary>
    /// Blocks a card by its card number.
    /// </summary>
    /// <param name="cardNumber">The card number to block.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<int> BlockCardAsync(int cardNumber)
    {
        var card = new CardEntity() { CardNumber = cardNumber, IsBlocked = true };
        context.Cards.Attach(card);
        context.Entry(card).Property(x => x.IsBlocked).IsModified = true;
        return await context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Increments the failed attempts count for the specified card.
    /// </summary>
    /// <param name="card">The card entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task IncrementFailedAttemptsAsync(CardEntity card)
    {
        context.Cards.Attach(card);
        context.Entry(card).Property(x => x.FailedAttempts).IsModified = true;
        await context.SaveChangesAsync();
    }
}