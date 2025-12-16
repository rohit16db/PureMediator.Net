using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PureMediator.Net.Abstractions.Pipeline;

namespace PureMediator.Net.Pipeline.Logging
{
    /// <summary>
    /// Represents a pipeline behavior that logs the lifecycle of request handling,
    /// including when a request starts processing and when it completes.
    /// This behavior can be used to trace and debug request execution within the mediator pipeline.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of the request being handled. Must implement <see cref="PureMediator.Net.Abstractions.Requests.IRequest{TResponse}"/>.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of the response returned by the request.
    /// </typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : PureMediator.Net.Abstractions.Requests.IRequest<TResponse>
    {
        /// <summary>
        /// The logger instance used to record informational messages about request handling.
        /// </summary>
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger{TCategoryName}"/> implementation used for logging request lifecycle events.
        /// </param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the specified request by logging its start and completion.
        /// This method is invoked as part of the mediator pipeline and allows logging before and after the next delegate is executed.
        /// </summary>
        /// <param name="request">
        /// The request instance being processed.
        /// </param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <param name="next">
        /// A delegate representing the next step in the pipeline, which returns a <see cref="Task{TResponse}"/> for the response.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResponse}"/> representing the asynchronous operation, containing the response from the next pipeline delegate.
        /// </returns>
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            Func<Task<TResponse>> next)
        {
            _logger.LogInformation("Handling request of type {RequestType}", typeof(TRequest).Name);
            var result = await next();
            _logger.LogInformation("Handled request of type {RequestType}", typeof(TRequest).Name);
            return result;
        }
    }
}
