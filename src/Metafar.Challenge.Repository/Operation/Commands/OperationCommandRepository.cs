using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Base;

namespace Metafar.Challenge.Repository.Operation.Commands;

public class OperationCommandRepository(MetafarDbContext context) : EntityFrameworkBaseRepository<OperationEntity>(context), IOperationCommandRepository
{

    /// <summary>
    /// Inserts a new operation into the database.
    /// </summary>
    /// <param name="operation">The operation entity to be inserted.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    public async Task<OperationEntity> InsertOperationAsync(OperationEntity operation)
    {
         await context.AddAsync(operation);
         return operation;
    }
}