using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;
using Metafar.Challenge.Repository.Queries.Card;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Queries.Account;

/// <summary>
/// Implementation of the account query repository.
/// </summary>
public class AccountQueryRepository(MetafarDbContext context) : EntityFrameworkBaseRepository<AccountEntity>(context), IAccountQueryRepository
{
    /// <summary>
    /// Retrieves an account by its card number, including user data.
    /// </summary>
    /// <param name="cardNumber">The card number to search for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the account entity if found; otherwise, null.</returns>
    public async Task<AccountEntity?> GetAccountByCardNumberAsync(int cardNumber)
    {
        return await context.Set<AccountEntity>()
            .AsNoTracking()
            .Include(a => a.User)
            .Include(a => a.Operations
                .Where(o => o.OperationType == "Withdrawal")
                .OrderByDescending(o => o.CreatedDate)
                .Take(1))
            .SingleOrDefaultAsync(a => a.Cards.Any(c => c.CardNumber == cardNumber));
    }
}