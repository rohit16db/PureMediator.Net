using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PureMediator.Net.DependencyInjection;
using PureMediator.Net.Abstractions;
using Xunit;

namespace PureMediator.Net.Tests.DependencyInjection
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddPureMediator_RegistersMediator()
        {
            var services = new ServiceCollection();
            services.AddPureMediator(typeof(ServiceCollectionExtensionsTests));
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetService<IMediator>();
            Assert.NotNull(mediator);
        }
        [Fact]
        public void AddPipelineBehavior_RegistersBehavior()
        {
            var services = new ServiceCollection();
            services.AddPureMediator(typeof(ServiceCollectionExtensionsTests));
            services.AddPipelineBehavior(typeof(DummyBehavior<,>));
            var provider = services.BuildServiceProvider();
            var behaviors = provider.GetServices(typeof(PureMediator.Net.Abstractions.Pipeline.IPipelineBehavior<DummyRequest, string>));
            Assert.Contains(behaviors, b => b.GetType().GetGenericTypeDefinition() == typeof(DummyBehavior<,>));
        }
        private class DummyRequest : PureMediator.Net.Abstractions.Requests.IRequest<string> { }
        private class DummyBehavior<TRequest, TResponse> : PureMediator.Net.Abstractions.Pipeline.IPipelineBehavior<TRequest, TResponse>
            where TRequest : PureMediator.Net.Abstractions.Requests.IRequest<TResponse>
        {
            public System.Threading.Tasks.Task<TResponse> Handle(TRequest request, System.Threading.CancellationToken cancellationToken, System.Func<System.Threading.Tasks.Task<TResponse>> next)
                => next();
        }
    }
}
