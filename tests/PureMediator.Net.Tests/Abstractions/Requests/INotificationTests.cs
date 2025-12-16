using System.Threading;
using System.Threading.Tasks;
using PureMediator.Net.Abstractions.Requests;
using Xunit;

namespace PureMediator.Net.Tests.Abstractions.Requests
{
    public class INotificationTests
    {
        private class DummyNotification : INotification { }
        [Fact]
        public void Can_Instantiate_Notification()
        {
            var n = new DummyNotification();
            Assert.NotNull(n);
        }
    }
}
