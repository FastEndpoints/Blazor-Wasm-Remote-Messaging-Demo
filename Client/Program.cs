using bWasm.Client;
using bWasm.Shared;
using FastEndpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var bld = WebAssemblyHostBuilder.CreateDefault(args);
bld.RootComponents.Add<App>("#app");
bld.RootComponents.Add<HeadOutlet>("head::after");

var app = bld.Build();
app.Services.MapRemoteCore(
    "https://localhost:7012",
    c =>
    {
        c.Register<CreateOrderCommand, CreateOrderResult>();
        c.Subscribe<OrderCreatedEvent, OrderCreatedEventHandler>();
    });
await app.RunAsync();

sealed class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
{
    internal static string? Result { get; private set; }

    public Task HandleAsync(OrderCreatedEvent e, CancellationToken c)
    {
        Result = $"{e.Description} Order Id: {e.OrderId}";

        return Task.CompletedTask;
    }
}