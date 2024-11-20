using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;

namespace Metafar.Challenge.Repository.Account.Commands;

public class AccountCommandRepository(MetafarDbContext context) : IAccountCommandRepository
{
    /// <summary>
    /// Updates the balance of the specified account and sets the updated date to the current UTC time.
    /// </summary>
    /// <param name="account">The account entity with the updated balance.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> UpdateAccountBalanceAsync(AccountEntity account)
    {
        account.UpdatedDate = DateTime.UtcNow;
        context.Accounts.Attach(account);
        context.Entry(account).Property(x => x.Balance).IsModified = true;
        context.Entry(account).Property(x => x.UpdatedDate).IsModified = true;
        return await context.SaveChangesAsync();
    }
}