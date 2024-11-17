using Metafar.Challenge.Dto;
using Metafar.Challenge.Infrastructure.Extensions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Account.Queries;
using Metafar.Challenge.Repository.Card.Commands;
using Metafar.Challenge.Repository.Queries.Card;
using Metafar.Challenge.UseCase.Account.Queries.GetAccountInformationByCardNumber;
using Metafar.Challenge.UseCase.Automapper;
using Metafar.Challenge.UseCase.Security.Queries.SignInUserByCard;

var builder = WebApplication.CreateBuilder(args);

// Global configuration
ServiceExtension.SetGlobalConfiguration(builder);

// Add Repository services
builder.Services.AddScoped<ICardQueryRepository, CardQueryRepository>();
builder.Services.AddScoped<ICardCommandRepository, CardCommandRepository>();
builder.Services.AddScoped<IAccountQueryRepository, AccountQueryRepository>();

// Add auto-mapper profiles
builder.Services.AddAutoMapper(typeof(ChallengeMapperProfile));

// Add UseCase services for UserLogin
builder.Services.AddScoped<ResponseModel<TokenDto>>();
builder.Services.AddScoped<SignInUserByCardQuery>();
builder.Services.AddScoped<SignInUserByCardHandler>();
builder.Services.AddScoped<SignInUserByCardValidator>();

// add use case services for GetAccountInfoByCardNumber
builder.Services.AddScoped<ResponseModel<AccountUserDto>>();
builder.Services.AddScoped<GetAccountInfoByCardNumberQuery>();
builder.Services.AddScoped<GetAccountInformationByCardNumberHandler>();

var app = builder.Build();
ApplicationBuilderExtension.SetGlobalApplicationBuilder(app);