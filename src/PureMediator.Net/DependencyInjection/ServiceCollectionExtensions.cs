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
    public static IServiceCollection AddPureMediator(this IServiceCollection services, params Type[] markerTypes)
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
    /// Registers a pipeline behavior of the specified type in the service collection.
    /// </summary>
    /// <remarks>
    /// This method adds the provided <paramref name="behaviorType"/> as a transient implementation of <see cref="IPipelineBehavior{TRequest, TResponse}"/>.
    /// Pipeline behaviors allow for cross-cutting concerns (such as logging, validation, or performance monitoring) to be executed as part of the request handling pipeline.
    /// </remarks>
    /// <param name="services">The service collection to which the pipeline behavior will be added. Must not be null.</param>
    /// <param name="behaviorType">The type of the pipeline behavior to register. Must implement <see cref="IPipelineBehavior{TRequest, TResponse}"/>.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance provided in <paramref name="services"/>, allowing for method chaining.</returns>
    public static IServiceCollection AddPipelineBehavior(
        this IServiceCollection services,
        Type behaviorType)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), behaviorType);
        return services;
    }
}
