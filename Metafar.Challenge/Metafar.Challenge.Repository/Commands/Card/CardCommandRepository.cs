using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;

namespace Metafar.Challenge.Repository.Commands.Card;

public class CardCommandRepository(MetafarDbContext context) : EntityFrameworkBaseRepository<CardEntity>(context), ICardCommandRepository
{
    /// <summary>
    /// Asynchronously blocks the specified card.
    /// </summary>
    /// <param name="card">The card entity to be blocked.</param>
    public async Task<int> BlockCardAsync(CardEntity card)
    {
        card.IsBlocked = true;
        card.UpdatedDate = DateTime.UtcNow;
        context.Cards.Attach(card);
        context.Entry(card).Property(x => x.IsBlocked).IsModified = true;
        context.Entry(card).Property(x => x.UpdatedDate).IsModified = true;
        return await context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Increments the failed attempts count for the specified card.
    /// </summary>
    /// <param name="card">The card entity to update.</param>
    public async Task IncrementFailedAttemptsAsync(CardEntity card)
    {
        card.FailedAttempts++;
        card.UpdatedDate = DateTime.UtcNow;
        context.Cards.Attach(card);
        context.Entry(card).Property(x => x.FailedAttempts).IsModified = true;
        context.Entry(card).Property(x => x.UpdatedDate).IsModified = true;
        await context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Resets the failed attempts count to zero for the specified card.
    /// </summary>
    /// <param name="card">The card entity to update.</param>
    public async Task ResetFailedAttemptsAsync(CardEntity card)
    {
        card.FailedAttempts = 0;
        card.UpdatedDate = DateTime.UtcNow;
        context.Cards.Attach(card);
        context.Entry(card).Property(x => x.FailedAttempts).IsModified = true;
        context.Entry(card).Property(x => x.UpdatedDate).IsModified = true;
        await context.SaveChangesAsync();
    }
}