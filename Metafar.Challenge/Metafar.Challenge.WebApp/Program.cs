using Blazored.LocalStorage;
using Metafar.Challenge.WebApp.Components;
using Metafar.Challenge.WebApp.Services;
using Metafar.Challenge.WebApp.ViewModel;

var builder = WebApplication.CreateBuilder(args);



// configure metafar services
builder.Services.AddHttpClient<MetafarService>(client =>
{
    client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("METAFAR_URL_BASE"));
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add local storage managment with custom options
builder.Services.AddBlazoredLocalStorage();

// Add services for view models
builder.Services.AddScoped<LoginViewModel>();
builder.Services.AddScoped<AccountViewModel>();
builder.Services.AddScoped<OperationViewModel>();
builder.Services.AddScoped<WithdrawViewModel>();

// add blazor bootstrapper
builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseStatusCodePagesWithReExecute("/errors/{0}");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}else{
    app.UseExceptionHandler("/errors/500");
    app.UseStatusCodePagesWithReExecute("/errors/{0}");
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();