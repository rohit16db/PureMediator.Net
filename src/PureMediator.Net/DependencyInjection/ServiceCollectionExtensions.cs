using Microsoft.Extensions.DependencyInjection;
using PureMediator.Net.Abstractions;
using PureMediator.Net.Abstractions.Pipeline;
using PureMediator.Net.Abstractions.Requests;
using PureMediator.Net.Core;
using Scrutor;

namespace PureMediator.Net.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring PureMediator.Net services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers PureMediator.Net services and handlers with the specified service collection, enabling mediator-based
    /// request and notification handling.
    /// </summary>
    /// <remarks>
    /// This method scans the assemblies of the specified marker types to automatically register
    /// implementations of <see cref="PureMediator.Net.Abstractions.Requests.IRequestHandler{TRequest, TResponse}"/>, <see cref="PureMediator.Net.Abstractions.Requests.INotificationHandler{TNotification}"/>,
    /// and <see cref="PureMediator.Net.Abstractions.Pipeline.IValidator{TRequest}"/> as transient services. The <see cref="IMediator"/> service is registered as a singleton.
    /// This enables dependency injection of mediator and handler services throughout the application.
    /// </remarks>
    /// <param name="services">The service collection to which PureMediator.Net services and handlers will be added. Must not be null.</param>
    /// <param name="markerTypes">An array of types used to identify assemblies for scanning and registration of handlers and validators. Each
    /// type's assembly will be scanned for relevant implementations.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance provided in <paramref name="services"/>, allowing for method chaining.</returns>
    public static IServiceCollection AddOpenMediator(this IServiceCollection services, params Type[] markerTypes)
    {
        services.AddSingleton<PureMediator.Net.Abstractions.IMediator, PureMediator.Net.Core.Mediator>();

        services.Scan(scan => scan
            .FromAssemblies(markerTypes.Select(t => t.Assembly))
            .AddClasses(c => c.AssignableTo(typeof(PureMediator.Net.Abstractions.Requests.IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            .AddClasses(c => c.AssignableTo(typeof(PureMediator.Net.Abstractions.Requests.INotificationHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            .AddClasses(c => c.AssignableTo(typeof(PureMediator.Net.Abstractions.Pipeline.IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );

        return services;
    }
   
    /// <summary>
    /// Registers a pipeline behavior of type <typeparamref name="TBehavior"/> in the service collection.
    /// </summary>
    /// <remarks>
    /// This method adds the specified pipeline behavior as a transient service for all <see cref="PureMediator.Net.Abstractions.Pipeline.IPipelineBehavior{TRequest, TResponse}"/>
    /// implementations. Pipeline behaviors are executed as part of the request handling pipeline, allowing for cross-cutting
    /// concerns such as logging, validation, or performance monitoring to be injected into the mediator's request processing.
    /// </remarks>
    /// <typeparam name="TBehavior">
    /// The type of the pipeline behavior to register. Must implement <see cref="PureMediator.Net.Abstractions.Pipeline.IPipelineBehavior{TRequest, TResponse}"/>.
    /// </typeparam>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the pipeline behavior will be added. Must not be null.
    /// </param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance provided in <paramref name="services"/>, allowing for method chaining.
    /// </returns>
    public static IServiceCollection AddPipelineBehavior<TBehavior>(this IServiceCollection services)
        where TBehavior : class
    {
        services.AddTransient(typeof(PureMediator.Net.Abstractions.Pipeline.IPipelineBehavior<,>), typeof(TBehavior));
        return services;
    }
}
