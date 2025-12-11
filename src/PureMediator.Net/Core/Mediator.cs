using Microsoft.Extensions.DependencyInjection;
using PureMediator.Net.Abstractions;
using PureMediator.Net.Abstractions.Pipeline;
using PureMediator.Net.Abstractions.Requests;

namespace PureMediator.Net.Core;

/// <summary>
/// Provides an implementation of the mediator pattern for handling requests and notifications.
/// This class coordinates the dispatching of requests to their respective handlers and manages pipeline behaviors.
/// </summary>
public class Mediator : IMediator
{
    /// <summary>
    /// The service provider used to resolve handlers and pipeline behaviors.
    /// </summary>
    private readonly IServiceProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mediator"/> class.
    /// </summary>
    /// <param name="provider">The <see cref="IServiceProvider"/> used to resolve dependencies.</param>
    public Mediator(IServiceProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Sends a request to the mediator and returns a response.
    /// The request is processed through any registered pipeline behaviors before reaching the handler.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected from the request.</typeparam>
    /// <param name="request">The request instance to send.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task{TResponse}"/> representing the asynchronous operation, containing the response.</returns>
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _provider.GetRequiredService(handlerType);

        Func<Task<TResponse>> handlerDelegate = async () =>
        {
            var method = handlerType.GetMethod("Handle")!;
            return await (Task<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken })!;
        };

        var pipelineType = typeof(IPipelineBehavior<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        var behaviors = _provider.GetServices(pipelineType).Cast<dynamic>().Reverse().ToList();

        foreach (var behavior in behaviors)
        {
            var next = handlerDelegate;
            handlerDelegate = () => behavior.Handle((dynamic)request, cancellationToken, next);
        }

        return await handlerDelegate();
    }

    /// <summary>
    /// Publishes a notification to all registered notification handlers.
    /// </summary>
    /// <typeparam name="TNotification">The type of the notification to publish.</typeparam>
    /// <param name="notification">The notification instance to publish.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var handlers = _provider.GetServices<INotificationHandler<TNotification>>();

        foreach (var handler in handlers)
            await handler.Handle(notification, cancellationToken);
    }
}
