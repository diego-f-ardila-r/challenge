namespace Metafar.Challenge.Dto;

public class OperationDto
{
    /// <summary>
    /// Unique identifier for the operation.
    /// </summary>
    public Guid OperationId { get; set; }
    
    /// <summary>
    /// Identifier for the account associated with the operation.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Type of the operation (DEPOSIT, WITHDRAWAL, TRANSFER).
    /// </summary>
    public string OperationType { get; set; }

    /// <summary>
    /// Amount of the operation.
    /// </summary>
    public double Amount { get; set; }
    
        /// <summary>
    /// Date and time when the operation was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }
}