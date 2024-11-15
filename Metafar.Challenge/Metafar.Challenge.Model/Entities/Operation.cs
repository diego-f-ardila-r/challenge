using System.ComponentModel.DataAnnotations;

namespace Metafar.Challenge.Model.Entities;

/// <summary>
/// Represents an operation entity.
/// </summary>
public class Operation : BaseEntity
{
    /// <summary>
    /// Unique identifier for the operation.
    /// </summary>
    [Key]
    public Guid OperationId { get; set; }

    /// <summary>
    /// Identifier for the account associated with the operation.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Type of the operation (DEPOSIT or WITHDRAW).
    /// </summary>
    public string OperationType { get; set; }

    /// <summary>
    /// Amount of the operation.
    /// </summary>
    public decimal Amount { get; set; }
}