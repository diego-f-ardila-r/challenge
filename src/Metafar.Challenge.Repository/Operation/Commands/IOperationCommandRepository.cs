using Metafar.Challenge.Entity;
using Metafar.Challenge.Repository.Core;

namespace Metafar.Challenge.Repository.Operation.Commands;

public interface IOperationCommandRepository : IBaseRepository<OperationEntity>
{
    /// <summary>
    /// Inserts a new operation into the database.
    /// </summary>
    /// <param name="operation">The operation entity to be inserted.</param>
    /// <returns>The operation id</returns>
    Task<OperationEntity> InsertOperationAsync(OperationEntity operation);
}