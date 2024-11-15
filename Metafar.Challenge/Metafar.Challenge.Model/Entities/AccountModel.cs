using System.ComponentModel.DataAnnotations;

namespace Metafar.Challenge.Model.Entities;

/// <summary>
/// Represents an account entity.
/// </summary>
public class AccountModel : BaseEntity
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
    public decimal Balance { get; set; }

    /// <summary>
    /// User associated with the account.
    /// </summary>
    public UserModel? User { get; set; }

    /// <summary>
    /// The cards associated with the account.
    /// </summary>
    public ICollection<CardModel> Cards { get; set; } = [];
}