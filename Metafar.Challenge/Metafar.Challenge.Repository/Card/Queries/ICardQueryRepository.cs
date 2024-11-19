using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Core;

namespace Metafar.Challenge.Repository.Queries.Card;

public interface ICardQueryRepository : IBaseRepository<CardEntity>
{
    /// <summary>
    /// Retrieves a card by its number.
    /// </summary>
    /// <param name="cardNumber">The card number.</param>
    /// <param name="pin">The card PIN.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the card model if found; otherwise, null.</returns>
    Task<CardEntity?> GetCardByNumberAsync(int cardNumber);
}