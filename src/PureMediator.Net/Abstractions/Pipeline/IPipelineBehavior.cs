using System;
using System.Threading;
using System.Threading.Tasks;

namespace PureMediator.Net.Abstractions.Pipeline
{
    /// <summary>
    /// Defines a pipeline behavior that can be executed before and/or after a request handler.
    /// Pipeline behaviors allow for cross-cutting concerns such as logging, validation, or authorization.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being handled.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the request.</typeparam>
    public interface IPipelineBehavior<TRequest, TResponse>
        where TRequest : PureMediator.Net.Abstractions.Requests.IRequest<TResponse>
    {
        /// <summary>
        /// Handles the request by invoking custom logic before and/or after the next delegate in the pipeline.
        /// </summary>
        /// <param name="request">The request instance being handled.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <param name="next">The next delegate in the pipeline to invoke.</param>
        /// <returns>The response from the next pipeline delegate.</returns>
        Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            Func<Task<TResponse>> next);
    }
}
