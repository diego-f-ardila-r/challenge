using Metafar.Challenge.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

ServiceExtension.SetGlobalConfiguration(builder);

var app = builder.Build();

ApplicationBuilderExtension.SetGlobalApplicationBuilder(app);