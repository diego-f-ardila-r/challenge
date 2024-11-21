using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Metafar.Challenge.Infrastructure.Configurations;

public class SwaggerConfigurationUtility
{
    private static OpenApiSecurityScheme Scheme => new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme,
        },
    };

    public static void Configure(SwaggerGenOptions options, string name, string title, string version)
    {
        options.ResolveConflictingActions(apiDesc => apiDesc.First());
        options.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = version });
        options.AddSecurityDefinition(Scheme.Reference.Id, Scheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { Scheme, Array.Empty<string>() },
        });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{name}.xml";
        
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        
        // validate if the xml file exists
        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
}