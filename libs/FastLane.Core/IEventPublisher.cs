using System.Threading;
using System.Threading.Tasks;

namespace FastLane.Core
{
    /// <summary>
    /// Defines a simple contract for publishing domain events asynchronously.
    /// Implementations may forward events to message buses, internal handlers or external systems.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publishes a domain event for handling.
        /// </summary>
        /// <typeparam name="TEvent">The event type derived from DomainEvent.</typeparam>
        /// <param name="domainEvent">The event instance.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task representing the asynchronous publish operation.</returns>
        Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken) where TEvent : DomainEvent;
    }
}
