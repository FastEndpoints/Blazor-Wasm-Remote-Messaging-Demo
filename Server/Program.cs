using bWasm.Shared;
using FastEndpoints;

var bld = WebApplication.CreateBuilder(args);
bld.AddHandlerServer();

var app = bld.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseGrpcWeb(new() { DefaultEnabled = true }); //enable grpc-web globally
app.MapHandlers(
    h =>
    {
        h.Register<CreateOrderCommand, CreateOrderCommandHandler, CreateOrderResult>();
        h.RegisterEventHub<OrderCreatedEvent>();
    });
app.MapFallbackToFile("index.html");
app.Run();

public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public Task<CreateOrderResult> ExecuteAsync(CreateOrderCommand cmd, CancellationToken ct)
    {
        _ = Task.Run(
            async () =>
            {
                //broadcast an event after 3 seconds

                await Task.Delay(3000);
                new OrderCreatedEvent
                {
                    OrderId = cmd.OrderId,
                    Description = "Successfully created"
                }.Broadcast(ct);
            },
            ct);

        return Task.FromResult(
            new CreateOrderResult
            {
                Message = $"Order {cmd.OrderId} created for {cmd.CustomerName}"
            });
    }
}