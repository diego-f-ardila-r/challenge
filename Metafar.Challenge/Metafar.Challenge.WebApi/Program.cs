using Metafar.Challenge.Dto;
using Metafar.Challenge.Infrastructure.Extensions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Commands;
using Metafar.Challenge.Repository.Queries;
using Metafar.Challenge.UseCase.Security.Queries.Authentication;
using Metafar.Challenge.UseCase.Users.Queries.UserLgoin;

var builder = WebApplication.CreateBuilder(args);

// Global configuration
ServiceExtension.SetGlobalConfiguration(builder);

// Add Repository services
builder.Services.AddScoped<ICardQueryRepository, CardQueryRepository>();
builder.Services.AddScoped<ICardCommandRepository, CardCommandRepository>();

// Add UseCase services for UserLogin
builder.Services.AddScoped<ResponseModel<TokenDto>>();
builder.Services.AddScoped<AuthenticationQuery>();
builder.Services.AddScoped<AuthenticationHandler>();
builder.Services.AddScoped<AuthenticationValidator>();

var app = builder.Build();
ApplicationBuilderExtension.SetGlobalApplicationBuilder(app);