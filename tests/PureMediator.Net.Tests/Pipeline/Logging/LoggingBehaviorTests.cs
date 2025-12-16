using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PureMediator.Net.Pipeline.Logging;
using PureMediator.Net.Abstractions.Pipeline;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Pipeline.Logging
{
    public class LoggingBehaviorTests
    {
        private class DummyRequest : IRequest<string> { }
        private class DummyLogger<T> : ILogger<T>
        {
            public bool WasCalled = false;
            public IDisposable BeginScope<TState>(TState state) => null;
            public bool IsEnabled(LogLevel logLevel) => true;
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                WasCalled = true;
            }
        }
        [Fact]
        public async Task Handle_Logs_And_CallsNext()
        {
            var logger = new DummyLogger<LoggingBehavior<DummyRequest, string>>();
            var behavior = new LoggingBehavior<DummyRequest, string>(logger);
            var result = await behavior.Handle(new DummyRequest(), CancellationToken.None, () => Task.FromResult("ok"));
            Assert.Equal("ok", result);
            Assert.True(logger.WasCalled);
        }
    }
}
