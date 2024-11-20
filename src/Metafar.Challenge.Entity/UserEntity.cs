using System.ComponentModel.DataAnnotations;

namespace Metafar.Challenge.Entity;

/// <summary>
/// Represents a user model entity.
/// </summary>
public class UserEntity : BaseEntity
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    [Key]
    public Guid UserId { get; set; }
    
    /// <summary>
    /// The username of the user.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// The collection of accounts associated with the user.
    /// </summary>
    public ICollection<AccountEntity> Accounts { get; set; } = [];
}