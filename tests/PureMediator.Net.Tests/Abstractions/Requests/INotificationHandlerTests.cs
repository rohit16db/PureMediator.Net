using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Abstractions.Requests
{
    public class INotificationHandlerTests
    {
        private class DummyNotification : INotification { }
        private class DummyHandler : INotificationHandler<DummyNotification>
        {
            public bool WasCalled = false;
            public Task Handle(DummyNotification notification, CancellationToken cancellationToken)
            {
                WasCalled = true;
                return Task.CompletedTask;
            }
        }
        [Fact]
        public async Task Handle_SetsWasCalled()
        {
            var handler = new DummyHandler();
            await handler.Handle(new DummyNotification(), CancellationToken.None);
            Assert.True(handler.WasCalled);
        }
    }
}
