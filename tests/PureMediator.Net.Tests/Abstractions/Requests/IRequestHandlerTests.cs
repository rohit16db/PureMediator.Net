using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Abstractions.Requests
{
    public class IRequestHandlerTests
    {
        private class DummyRequest : IRequest<string> { }
        private class DummyHandler : IRequestHandler<DummyRequest, string>
        {
            public Task<string> Handle(DummyRequest request, CancellationToken cancellationToken)
                => Task.FromResult("ok");
        }
        [Fact]
        public async Task Handle_ReturnsOk()
        {
            var handler = new DummyHandler();
            var result = await handler.Handle(new DummyRequest(), CancellationToken.None);
            Assert.Equal("ok", result);
        }
    }
}
