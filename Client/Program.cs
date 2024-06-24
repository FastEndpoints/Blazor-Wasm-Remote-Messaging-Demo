using bWasm.Client;
using bWasm.Shared;
using FastEndpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var bld = WebAssemblyHostBuilder.CreateDefault(args);
bld.RootComponents.Add<App>("#app");
bld.RootComponents.Add<HeadOutlet>("head::after");

var app = bld.Build();
app.Services.MapRemote(
    "https://localhost:7012",
    c =>
    {
        c.Register<CreateOrderCommand, CreateOrderResult>();
    });
await app.RunAsync();