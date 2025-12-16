using System;
using PureMediator.Net.Abstractions.Pipeline;
using Xunit;

namespace PureMediator.Net.Tests.Abstractions.Pipeline
{
    public class IValidatorTests
    {
        private class DummyValidator : IValidator<string>
        {
            public bool WasCalled = false;
            public void Validate(string instance)
            {
                WasCalled = true;
                if (string.IsNullOrEmpty(instance))
                    throw new ArgumentException();
            }
        }
        [Fact]
        public void Validate_Throws_On_Empty()
        {
            var validator = new DummyValidator();
            Assert.Throws<ArgumentException>(() => validator.Validate(""));
            Assert.True(validator.WasCalled);
        }
        [Fact]
        public void Validate_DoesNotThrow_On_Valid()
        {
            var validator = new DummyValidator();
            validator.Validate("ok");
            Assert.True(validator.WasCalled);
        }
    }
}
