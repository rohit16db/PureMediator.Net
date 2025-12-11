using Microsoft.Extensions.DependencyInjection;
using PureMediator.Net.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace PureMediator.Tests;

public class MediatorTests
{
    //[Fact]
    //public async Task Should_Invoke_Handler()
    //{
    //    var services = new ServiceCollection();
    //    services.AddOpenMediator(typeof(MediatorTests));

    //    var provider = services.BuildServiceProvider();
    //    var mediator = provider.GetRequiredService<OpenMediator.Abstractions.IMediator>();

    //    var result = await mediator.Send(new TestRequest("hi"));
    //    Assert.Equal("hi processed", result);
    //}
}

public record TestRequest(string Text) : PureMediator.Net.Abstractions.Requests.IRequest<string>;
//public class TestRequestHandler : PureMediator.Net.Abstractions.Requests.IRequestHandler<TestRequest, string>
//{
//    public Task<string> Handle(TestRequest request, CancellationToken cancellationToken)
//    {
//        return Task.FromResult(request.Text + " processed");
//    }
//}
