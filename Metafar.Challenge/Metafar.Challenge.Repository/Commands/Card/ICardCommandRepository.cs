using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Core;

namespace Metafar.Challenge.Repository.Commands.Card;

public interface ICardCommandRepository : IBaseRepository<CardEntity>
{
    /// <summary>
    /// Asynchronously blocks the specified card.
    /// </summary>
    /// <param name="card">The card entity to be blocked.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result as an integer.</returns>
    Task<int> BlockCardAsync(CardEntity card);

    /// <summary>
    /// Increments the number of failed attempts for a given card.
    /// </summary>
    /// <param name="card">The card entity for which to increment failed attempts.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task IncrementFailedAttemptsAsync(CardEntity card);
    
    /// <summary>
    // /// Resets the failed attempts count to zero for the specified card.
    // /// </summary>
    // /// <param name="card">The card entity to update.</param>
    Task ResetFailedAttemptsAsync(CardEntity card);
}