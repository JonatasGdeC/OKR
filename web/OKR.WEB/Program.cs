using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OKR.WEB;
using OKR.WEB.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStatesProvider>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthTokenHandler>();
builder.Services.AddMudServices();


//Conection API
builder.Services.AddHttpClient("API", client =>
{
  client.BaseAddress = new Uri("http://localhost:5135/"); // ou o endereço da sua API
}).AddHttpMessageHandler<AuthTokenHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

//Services
builder.Services.AddScoped<AuthStatesProvider>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ObjectiveService>();
builder.Services.AddScoped<KeyResultService>();

await builder.Build().RunAsync();
