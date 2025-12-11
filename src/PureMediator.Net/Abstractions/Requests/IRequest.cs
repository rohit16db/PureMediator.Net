namespace PureMediator.Net.Abstractions.Requests;

/// <summary>
/// Represents a request that expects a response from a handler when sent through the mediator.
/// Implement this interface to define a request type for use with the mediator.
/// </summary>
/// <typeparam name="TResponse">The type of the response expected from the request.</typeparam>
public interface IRequest<TResponse> { }
