using System.ComponentModel.DataAnnotations;

namespace Metafar.Challenge.Entity;

/// <summary>
/// Represents an account entity.
/// </summary>
public class AccountEntity : BaseEntity
{
    /// <summary>
    /// The unique identifier for the account.
    /// </summary>
    [Key]
    public Guid AccountId { get; set; }

    /// <summary>
    /// Identifier for the user associated with the account.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The account number.
    /// </summary>
    public int AccountNumber { get; set; }

    /// <summary>
    /// Balance of the account.
    /// </summary>
    public double Balance { get; set; }

    /// <summary>
    /// User associated with the account.
    /// </summary>
    public UserEntity? User { get; set; }

    /// <summary>
    /// The cards associated with the account.
    /// </summary>
    public ICollection<CardEntity> Cards { get; set; } = [];
    
    public ICollection<OperationEntity> Operations { get; set; } = [];
}