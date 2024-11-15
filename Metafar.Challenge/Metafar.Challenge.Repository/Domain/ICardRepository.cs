using Metafar.Challenge.Model.Entities;
using Metafar.Challenge.Repository.Core;

namespace Metafar.Challenge.Repository.Domain;

public interface ICardRepository : ICoreRepository<CardModel>
{
    /// <summary>
    /// Retrieves a card by its card number and PIN.
    /// </summary>
    /// <param name="cardNumber">The card number.</param>
    /// <param name="pin">The card PIN.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the card model if found; otherwise, null.</returns>
    Task<CardModel?> GetCardByCardNumberAndPinAsync(int cardNumber, int pin);
    
    /// <summary>
    // /// Blocks a card by its card number.
    // /// </summary>
    // /// <param name="cardNumber">The card number to block.</param>
    // /// <returns>A task that represents the asynchronous operation.</returns>
    Task BlockCardAsync(int cardNumber);
}