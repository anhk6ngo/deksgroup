using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
var assemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddScoped<CustomHttpHandler>();
builder.Services.AddHttpClient("WebApi", sp =>
{
    sp.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    sp.Timeout = TimeSpan.FromMinutes(10);
}).AddHttpMessageHandler<CustomHttpHandler>();
builder.Services.AddScoped<IHttpServiceClient, HttpServiceClientClient>();
builder.Services.AddManagers(assemblies);
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddLocalization();
var host = builder.Build();
const string defaultCulture = "vi-VN";
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("appCulture.get");
var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);

if (result == null)
{
    await js.InvokeVoidAsync("appCulture.set", defaultCulture);
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
await host.RunAsync();