using System;

namespace OrderService.Contracts
{
    public interface IEvent
    {
        Guid Id { get; set; }
    }
    
    public class OrderPlacedEvent : IEvent
    {
        public string Value { get; set; }
        public Guid Id { get; set; }
    }
}