using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Core;

namespace Metafar.Challenge.Repository.Operation.Queries;

public interface IOperationQueryRepository : IBaseRepository<OperationEntity>
{
    /// <summary>
    /// Retrieves a paginated list of operations filtered by card number.
    /// </summary>
    /// <param name="cardNumber">The card number to filter operations by.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of operations per page.</param>
    /// <returns>A tuple containing the list of operations and the total number of pages.</returns>
    Task<(IEnumerable<OperationEntity> Operations, int TotalPages)> GetOperationsByCardNumberAsync(int cardNumber, int pageNumber, int pageSize);
}