#pragma warning disable CS8618

    using FastEndpoints;

    namespace bWasm.Shared
    {
        public class CreateOrderCommand : ICommand<CreateOrderResult>
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; }
        }

        public class CreateOrderResult
        {
            public string Message { get; set; }
        }
    }