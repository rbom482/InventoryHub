using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with proper base address and timeout
builder.Services.AddScoped(sp => 
{
    var httpClient = new HttpClient 
    { 
        BaseAddress = new Uri("https://localhost:7001/"),
        Timeout = TimeSpan.FromSeconds(30)
    };
    return httpClient;
});

// Add API service for centralized API calls
builder.Services.AddScoped<IApiService, ApiService>();

// Add logging for debugging
builder.Services.AddLogging();

await builder.Build().RunAsync();
