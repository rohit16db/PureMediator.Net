using System;
using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions.Pipeline;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Abstractions.Pipeline
{
    public class IPipelineBehaviorTests
    {
        private class DummyRequest : IRequest<string> { }
        private class DummyBehavior : IPipelineBehavior<DummyRequest, string>
        {
            public bool WasCalled = false;
            public async Task<string> Handle(DummyRequest request, CancellationToken cancellationToken, Func<Task<string>> next)
            {
                WasCalled = true;
                return await next();
            }
        }
        [Fact]
        public async Task Handle_CallsNext()
        {
            var behavior = new DummyBehavior();
            var result = await behavior.Handle(new DummyRequest(), CancellationToken.None, () => Task.FromResult("ok"));
            Assert.True(behavior.WasCalled);
            Assert.Equal("ok", result);
        }
    }
}
