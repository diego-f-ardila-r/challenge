namespace Metafar.Challenge.Model.Configurations;

public record JwtConfigurationModel
{
    public static string? Issuer { get; } = Environment.GetEnvironmentVariable("JWT_ISSUER");

    public static string? Secret { get; } = Environment.GetEnvironmentVariable("JWT_SECRET");

    public static string? Audience { get; } = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

    public static int? ExpireMinutes { get; } = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES"));
}