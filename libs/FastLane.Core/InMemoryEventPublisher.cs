using System.Threading.Tasks;

namespace FastLane.Core
{
    /// <summary>
    /// Simple in-memory event publisher that completes immediately.
    /// This implementation does not dispatch events to handlers but provides a placeholder for future expansion.
    /// </summary>
    public class InMemoryEventPublisher : IEventPublisher
    {
        public Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : DomainEvent
        {
            // TODO: In a real implementation, resolve handlers and invoke them
            return Task.CompletedTask;
        }
    }
}
