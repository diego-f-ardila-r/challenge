using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Metafar.Challenge.Model;
using Metafar.Challenge.Model.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Metafar.Challenge.Infrastructure.Utility;

public static class JwtTokenUtility
{
    /// <summary>
    /// Generates a JWT token.
    /// </summary>
    /// <param name="subject"> Subject</param>
    /// <param name="username">The username to include in the token claims.</param>
    /// <returns>A JWT token as a string.</returns>
    public static string GenerateJwtToken(string subject, string username)
    {
        // Set claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            new Claim(JwtRegisteredClaimNames.Name, username),
            // Add more claims if needed
        };
        
        // prepare the token
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfigurationModel.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: JwtConfigurationModel.Issuer,
            audience: JwtConfigurationModel.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(JwtConfigurationModel.ExpireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}