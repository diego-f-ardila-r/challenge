using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Metafar.Challenge.Infrastructure.Utility;

public static class JwtTokenUtility
{
    /// <summary>
    /// Generates a JWT token.
    /// </summary>
    /// <param name="secret">The secret key used for signing the token.</param>
    /// <param name="issuer">The issuer of the token.</param>
    /// <param name="audience">The audience for the token.</param>
    /// <param name="username">The username to include in the token claims.</param>
    /// <param name="expireMinutes">The expiration time in minutes for the token. Default is 60 minutes.</param>
    /// <returns>A JWT token as a string.</returns>
    public static string GenerateJwtToken(string secret, string issuer, string audience, string username, int expireMinutes = 60)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: issuer,
            audience: audience,
            claims: new[] { new Claim(ClaimTypes.Name, username) },
            expires: DateTime.Now.AddMinutes(expireMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}