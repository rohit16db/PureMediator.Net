using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Pipeline.Validation;
using PureMediator.Net.Abstractions.Pipeline;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Pipeline.Validation
{
    public class ValidationBehaviorTests
    {
        private class DummyRequest : IRequest<string> { }
        private class DummyValidator : IValidator<DummyRequest>
        {
            public void Validate(DummyRequest instance)
            {
                if (instance == null)
                    throw new ArgumentException();
            }
        }
        [Fact]
        public async Task Handle_Throws_On_Invalid()
        {
            var behavior = new ValidationBehavior<DummyRequest, string>(new List<IValidator<DummyRequest>> { new DummyValidator() });
            await Assert.ThrowsAsync<ArgumentException>(() => behavior.Handle(null, CancellationToken.None, () => Task.FromResult("ok")));
        }
        [Fact]
        public async Task Handle_CallsNext_On_Valid()
        {
            var behavior = new ValidationBehavior<DummyRequest, string>(new List<IValidator<DummyRequest>> { new DummyValidator() });
            var result = await behavior.Handle(new DummyRequest(), CancellationToken.None, () => Task.FromResult("ok"));
            Assert.Equal("ok", result);
        }
    }
}
