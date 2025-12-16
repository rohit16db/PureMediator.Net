using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PureMediator.Net.Core;
using PureMediator.Net.Abstractions;
using PureMediator.Net.Abstractions.Requests;
using PureMediator.Net.Abstractions.Pipeline;
using Xunit;

namespace PureMediator.Net.Tests.Core
{
    public class MediatorTests
    {
        private class DummyRequest : IRequest<string> { }
        private class DummyHandler : IRequestHandler<DummyRequest, string>
        {
            public Task<string> Handle(DummyRequest request, CancellationToken cancellationToken) => Task.FromResult("ok");
        }
        private class DummyNotification : INotification { }
        private class DummyNotificationHandler : INotificationHandler<DummyNotification>
        {
            public bool WasCalled = false;
            public Task Handle(DummyNotification notification, CancellationToken cancellationToken) { WasCalled = true; return Task.CompletedTask; }
        }
        [Fact]
        public async Task Send_InvokesHandler()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IRequestHandler<DummyRequest, string>, DummyHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = new Mediator(provider);
            var result = await mediator.Send<string>(new DummyRequest());
            Assert.Equal("ok", result);
        }
        [Fact]
        public async Task Publish_InvokesNotificationHandlers()
        {
            var services = new ServiceCollection();
            var handler = new DummyNotificationHandler();
            services.AddSingleton<INotificationHandler<DummyNotification>>(handler);
            var provider = services.BuildServiceProvider();
            var mediator = new Mediator(provider);
            await mediator.Publish(new DummyNotification());
            Assert.True(handler.WasCalled);
        }
    }
}
