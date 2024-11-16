using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Commands;

public class CardCommandRepository(RepositoryDbContext context) : EntityFrameworkBaseRepository<CardEntity>(context), ICardCommandRepository
{
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