using Metafar.Challenge.Entity;

namespace Metafar.Challenge.Repository.Account.Commands;

public interface IAccountCommandRepository
{
    /// <summary>
    /// Updates the balance of the specified account and sets the updated date to the current UTC time.
    /// </summary>
    /// <param name="account">The account entity with the updated balance.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> UpdateAccountBalanceAsync(AccountEntity account);
}