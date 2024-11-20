using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Operation.Queries;

public class OperationQueryRepository(MetafarDbContext context) : EntityFrameworkBaseRepository<OperationEntity>(context), IOperationQueryRepository
{
    /// <summary>
    /// Retrieves a paginated list of operations filtered by card number.
    /// </summary>
    /// <param name="cardNumber">The card number to filter operations by.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of operations per page.</param>
    /// <returns>A tuple containing the list of operations and the total number of pages.</returns>
    public async Task<(IEnumerable<OperationEntity> Operations, int TotalPages)> GetOperationsByCardNumberAsync(
        int cardNumber, 
        int pageNumber, 
        int pageSize)
    {
        // Query for operations with filtering and paging in a single query
        var query = context.Operations
            .Where(o => o.Account.Cards.Any(c => c.CardNumber == cardNumber))
            .OrderByDescending(o => o.CreatedDate);

        // Get total count and paginated data in a single trip to the database
        var totalOperations = await query.CountAsync();
        var operations = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Calculate total pages
        var totalPages = (int)Math.Ceiling(totalOperations / (double)pageSize);

        return (operations, totalPages);
    }
}