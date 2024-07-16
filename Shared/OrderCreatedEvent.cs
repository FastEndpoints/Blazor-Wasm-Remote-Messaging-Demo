#pragma warning disable CS8618

    using FastEndpoints;

    namespace bWasm.Shared
    {
        public sealed class OrderCreatedEvent : IEvent
        {
            public int OrderId { get; set; }
            public string Description { get; set; }
        }
    }