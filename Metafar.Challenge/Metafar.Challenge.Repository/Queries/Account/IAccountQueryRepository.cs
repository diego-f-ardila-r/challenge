using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Core;

namespace Metafar.Challenge.Repository.Queries.Account;

/// <summary>
/// Interface for account query repository.
/// </summary>
public interface IAccountQueryRepository : IBaseRepository<AccountEntity>
{
    /// <summary>
    /// Retrieves an account by its card number, including user data.
    /// </summary>
    /// <param name="cardNumber">The card number to search for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the account entity if found; otherwise, null.</returns>
    Task<AccountEntity?> GetAccountByCardNumberAsync(int cardNumber);
}