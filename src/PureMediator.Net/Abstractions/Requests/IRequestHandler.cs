using System.Threading;
using System.Threading.Tasks;

namespace PureMediator.Net.Abstractions.Requests
{
    /// <summary>
    /// Defines a handler for processing requests sent through the mediator.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request to handle.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the handler.</typeparam>
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles the specified request and returns a response.
        /// </summary>
        /// <param name="request">The request instance to handle.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResponse}"/> representing the asynchronous operation, containing the response.</returns>
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
