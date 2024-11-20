using Metafar.Challenge.Infrastructure.Middleware;
using Metafar.Challenge.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metafar.Challenge.Infrastructure.Extensions;

public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Set global application builder.
    /// </summary>
    public static void SetGlobalApplicationBuilder(WebApplication app)
    {
        // Apply migrations at startup
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MetafarDbContext>();
            dbContext.Database.Migrate();
        }
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Custom Middleware Setup
        app.AddCorrelationIdMiddleware();
      
        // Exception Handler Middleware Setup
        app.UseExceptionHandler();

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }

    private static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
}