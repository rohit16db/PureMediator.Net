using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions.Requests;

namespace PureMediator.Net.Abstractions
{
    /// <summary>
    /// Defines the contract for a mediator that handles requests and notifications.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Sends a request to the mediator and returns a response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response expected from the request.</typeparam>
        /// <param name="request">The request instance to send.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResponse}"/> representing the asynchronous operation, containing the response.</returns>
        Task<TResponse> Send<TResponse>(Requests.IRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a notification to all registered notification handlers.
        /// </summary>
        /// <typeparam name="TNotification">The type of the notification to publish.</typeparam>
        /// <param name="notification">The notification instance to publish.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : Requests.INotification;
    }
}
