using Metafar.Challenge.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Metafar.Challenge.Infrastructure.Extensions;

public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Set global application builder.
    /// </summary>
    public static void SetGlobalApplicationBuilder(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Custom Middleware Setup
        app.AddCorrelationIdMiddleware();

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }

    /// <summary>
    /// Set correlational id middleware.
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();

}