using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Metafar.Challenge.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Metafar.Challenge.Infrastructure.Utility;

public static class JwtTokenUtility
{
    /// <summary>
    /// Generates a JWT token.
    /// </summary>
    /// <param name="username">The username to include in the token claims.</param>
    /// <returns>A JWT token as a string.</returns>
    public static string GenerateJwtToken(string username)
    {
        // Get JWT Settings from environment variables
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        var expirationMinutes = double.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES"));
        
        // prepare the token
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        // create the token
        var token = new JwtSecurityToken(issuer: Environment.GetEnvironmentVariable(issuer),
            audience: Environment.GetEnvironmentVariable(audience),
            claims: new[] { new Claim(ClaimTypes.Name, username) },
            expires: DateTime.Now.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}