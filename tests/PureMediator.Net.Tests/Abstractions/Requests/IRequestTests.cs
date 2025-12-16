using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Abstractions.Requests
{
    public class IRequestTests
    {
        private class DummyRequest : IRequest<string> { }
        [Fact]
        public void Can_Instantiate_Request()
        {
            var req = new DummyRequest();
            Assert.NotNull(req);
        }
    }
}
