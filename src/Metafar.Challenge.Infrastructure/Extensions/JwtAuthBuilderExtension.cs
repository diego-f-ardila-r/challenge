using System.Text;
using Metafar.Challenge.Model.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Metafar.Challenge.Infrastructure.Extensions;

public static class JwtAuthBuilderExtension
{
    public static void AddJwtAuthentication(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();

        builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtConfigurationModel.Issuer,
                    ValidAudience = JwtConfigurationModel.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfigurationModel.Secret)),
                    RequireExpirationTime = true,
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        string authorization = context.Request.Headers["Authorization"];

                        if (string.IsNullOrEmpty(authorization))
                        {
                            context.NoResult();
                        }
                        else
                        {
                            context.Token = authorization.Replace("Bearer ", string.Empty);
                        }

                        return Task.CompletedTask;
                    },
                };
            });
    }
}