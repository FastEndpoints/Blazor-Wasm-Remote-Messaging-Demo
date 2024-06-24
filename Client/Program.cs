using bWasm.Client;
using bWasm.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FastEndpoints;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

app.Services.MapRemote(
    "https://localhost:7012",
    c =>
    {
        c.Register<CreateOrderCommand, CreateOrderResult>();
    });

await app.RunAsync();