using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LeuzeWeather;
using LeuzeWeather.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add MudBlazor services
builder.Services.AddMudServices();

// Add HttpClient services for GeocodingService and WeatherService
builder.Services.AddHttpClient("GeocodingClient", client =>
{
    client.BaseAddress = new Uri("https://geocoding-api.open-meteo.com/");
});
builder.Services.AddHttpClient("WeatherClient", client =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com/");
});

// Singletons, so that the cache can survive through NavigationManager events
builder.Services.AddSingleton<GeocodingService>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var client = factory.CreateClient("GeocodingClient");
    client.Timeout = TimeSpan.FromSeconds(10);
    return new GeocodingService(client);
});

builder.Services.AddSingleton<WeatherService>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var client = factory.CreateClient("WeatherClient");
    client.Timeout = TimeSpan.FromSeconds(10);
    return new WeatherService(client);
});

builder.Services.AddScoped<ComfortServiceCurrent>();
builder.Services.AddScoped<ComfortServiceForecast>();

await builder.Build().RunAsync();
