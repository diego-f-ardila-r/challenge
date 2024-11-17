namespace Metafar.Challenge.Dto;

public record AccountUserDto
{
    /// <summary>
    /// Gets or sets the account ID.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Gets or sets the account number.
    /// </summary>
    public string? AccountNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the account balance.
    /// </summary>
    public double Balance { get; set; }
    
    public DateTime LastWithdrawalDate { get; set; }
    
    /// <summary>
    /// Gets or sets the userId.
    /// </summary>
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// Gets or sets the user full name.
    /// </summary>
    public string? FullName { get; set; }
}