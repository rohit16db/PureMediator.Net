using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ConsoleSample.Queries;
using ConsoleSample.Validators;
using PureMediator.Net.DependencyInjection;
using PureMediator.Net.Pipeline.Validation;
using PureMediator.Net.Pipeline.Logging;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services.AddLogging(cfg => cfg.AddConsole());
        services.AddPureMediator(typeof(Program));
        services.AddPipelineBehavior(typeof(ValidationBehavior<,>));
        services.AddPipelineBehavior(typeof(LoggingBehavior<,>));
    })
    .Build();

var mediator = host.Services.GetRequiredService<PureMediator.Net.Abstractions.IMediator>();

var result = await mediator.Send(new GetUserQuery(0));
Console.WriteLine(result);
