using System.ComponentModel.DataAnnotations;

namespace Metafar.Challenge.Entity;

/// <summary>
/// Represents a card entity with associated properties.
/// </summary>
public class CardEntity : BaseEntity
{
    /// <summary>
    /// The unique identifier for the card.
    /// </summary>
    [Key]
    public Guid CardId { get; set; }

    /// <summary>
    /// Identifier for the associated account.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// The card number.
    /// </summary>
    public int CardNumber { get; set; }

    /// <summary>
    /// Access PIN for the card.
    /// </summary>
    public int AccessPin { get; set; }

    /// <summary>
    /// The number of failed attempts to access the card.
    /// </summary>
    public int FailedAttempts { get; set; }

    /// <summary>
    /// Indicates whether the card is blocked.
    /// </summary>
    public bool IsBlocked { get; set; }
    
    /// <summary>
    /// Associated account.
    /// </summary>
    public AccountEntity Account { get; set; }
}
