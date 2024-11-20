namespace Metafar.Challenge.Model;

public record AppSettingsModel
{
    public JwtToken? JwtToken { get; set; }
}

public record JwtToken
{
    public string? Secret { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}