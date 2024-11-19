using System.Text.Json;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using Metafar.Challenge.WebApp.Components;
using Metafar.Challenge.WebApp.Components.Shared;
using Metafar.Challenge.WebApp.Services;
using Metafar.Challenge.WebApp.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// configure metafar services
builder.Services.AddHttpClient<MetafarServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5000");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YjVkNDZlZC1kMmVmLTRhNzAtOWNjYS0zYzMzMTAwNTAwNDgiLCJuYW1lIjoiMTIzNDU2NzgiLCJleHAiOjE3MzE5ODUzMzMsImlzcyI6Ik1FVEFGQVIiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAvIn0.1ptzTLWqMomlOGfdzlhhWRAekpjhtn5GTw30K08c2cc");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add local storage managment with custom options
builder.Services.AddBlazoredLocalStorage();

// Add services for view models
builder.Services.AddScoped<AccountViewModel>();
builder.Services.AddScoped<OperationViewModel>();
builder.Services.AddScoped<WithdrawViewModel>();

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