using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions.Pipeline;

namespace PureMediator.Net.Pipeline.Validation
{
    /// <summary>
    /// Represents a pipeline behavior that performs validation on requests before they are handled.
    /// This behavior invokes all registered validators for the request type.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being validated.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the request.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : PureMediator.Net.Abstractions.Requests.IRequest<TResponse>
    {
        /// <summary>
        /// The collection of validators to apply to the request.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">The collection of validators for the request type.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handles the request by validating it with all registered validators before invoking the next pipeline delegate.
        /// </summary>
        /// <param name="request">The request instance to validate.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <param name="next">The next delegate in the pipeline to invoke.</param>
        /// <returns>The response from the next pipeline delegate.</returns>
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
        {
            foreach (var validator in _validators)
                validator.Validate(request);

            return next();
        }
    }
}
