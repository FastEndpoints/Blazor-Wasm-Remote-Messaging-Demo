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
        h.Register<CreateOrderCommand, CreateOrderHandler, CreateOrderResult>();
    });
app.MapFallbackToFile("index.html");
app.Run();

public sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public Task<CreateOrderResult> ExecuteAsync(CreateOrderCommand cmd, CancellationToken _)
        => Task.FromResult(
            new CreateOrderResult
            {
                Message = $"Order {cmd.OrderId} created for {cmd.CustomerName}"
            });
}