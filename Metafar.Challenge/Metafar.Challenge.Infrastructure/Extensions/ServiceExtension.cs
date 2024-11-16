using System.Text.Json;
using System.Text.Json.Serialization;
using Metafar.Challenge.Infrastructure.Handlers;
using Metafar.Challenge.Infrastructure.Utility;
using Metafar.Challenge.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metafar.Challenge.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void SetGlobalConfiguration(WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddCustomControllerConfiguration();

        // Automapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add OSS configuration
        builder.Services.AddErrorHandlerConfiguration();
        builder.Services.AddCustomConfiguration();

        // Set routing configuration
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

    }

    private static IServiceCollection AddCustomControllerConfiguration(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            // set content type for swagger requests
            options.Filters.Add(new ConsumesAttribute("application/json"));
            options.Filters.Add(new ProducesAttribute("application/json"));
        })
        .AddJsonOptions(options =>
        {
            //ignore values in null
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            // Set the response body properties in camelCase
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.WriteIndented = true;
        })
        .ConfigureApiBehaviorOptions(opt =>
        {
            // Configure behavior to prevent XSS vulnerability
            opt.InvalidModelStateResponseFactory = context =>
            {
                // Get instance to retrive the response model
                var responseResult = (ResponseModel<object>)context.HttpContext.RequestServices.GetService(typeof(ResponseModel<object>))!;
                responseResult?.SetFunctionalErrorResponse("VALIDATION_ERROR");

                // Map Errors 
                var errors = (from value in context.ModelState.Values from error in value.Errors select error.ErrorMessage).ToList();

                responseResult.Errors = errors;

                return new BadRequestObjectResult(responseResult);
            };
        });

        return services;
    }

    private static IServiceCollection AddCustomConfiguration(this IServiceCollection services)
    {
       // Response model
        services.AddScoped(typeof(ResponseModel<>));

        // Correlational Id generator
        services.AddScoped<CorrelationIdGeneratorUtility>();

        return services;
    }

    private static IServiceCollection AddErrorHandlerConfiguration(this IServiceCollection services)
    {
        services.AddExceptionHandler<FunctionalExceptionHandler> ();
        services.AddExceptionHandler<GlobalExceptionHandler> ();
        services.AddProblemDetails();
        return services;
    }
}