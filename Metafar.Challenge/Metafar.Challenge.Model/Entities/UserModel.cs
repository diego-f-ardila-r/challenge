using System.ComponentModel.DataAnnotations;

namespace Metafar.Challenge.Model.Entities;

/// <summary>
/// Represents a user model entity.
/// </summary>
public class UserModel : BaseEntity
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    [Key]
    public Guid UserId { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string LastName { get; set; }
}