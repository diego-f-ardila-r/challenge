using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Core;

namespace Metafar.Challenge.Repository.Commands;

public interface ICardCommandRepository : IBaseRepository<CardEntity>
{
    /// <summary>
    // /// Blocks a card by its card number.
    // /// </summary>
    // /// <param name="cardNumber">The card number to block.</param>
    // /// <returns>A task that represents the asynchronous operation.</returns>
    Task BlockCardAsync(int cardNumber);
}