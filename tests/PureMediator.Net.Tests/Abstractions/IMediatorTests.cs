using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Abstractions
{
    public class IMediatorTests
    {
        private class DummyMediator : IMediator
        {
            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
                => Task.FromResult(default(TResponse));
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
                where TNotification : INotification
                => Task.CompletedTask;
        }

        [Fact]
        public async Task Send_ReturnsDefault()
        {
            var mediator = new DummyMediator();
            var result = await mediator.Send<string>(new DummyRequest());
            Assert.Null(result);
        }

        [Fact]
        public async Task Publish_DoesNotThrow()
        {
            var mediator = new DummyMediator();
            await mediator.Publish(new DummyNotification());
        }

        private class DummyRequest : IRequest<string> { }
        private class DummyNotification : INotification { }
    }
}
