namespace PureMediator.Net.Abstractions.Requests;

/// <summary>
/// Defines a handler for processing notifications published through the mediator.
/// </summary>
/// <typeparam name="TNotification">The type of the notification to handle.</typeparam>
public interface INotificationHandler<TNotification>
    where TNotification : INotification
{
    /// <summary>
    /// Handles the specified notification.
    /// </summary>
    /// <param name="notification">The notification instance to handle.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Handle(TNotification notification, CancellationToken cancellationToken);
}
