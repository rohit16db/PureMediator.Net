using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenMediator.DependencyInjection;
using OpenMediator.Pipeline.Logging;
using OpenMediator.Pipeline.Validation;
using ConsoleSample.Queries;
using ConsoleSample.Validators;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services.AddLogging(cfg => cfg.AddConsole());
        services.AddOpenMediator(typeof(Program));
        services.AddPipelineBehavior<ValidationBehavior<,>>();
        services.AddPipelineBehavior<LoggingBehavior<,>>();
    })
    .Build();

var mediator = host.Services.GetRequiredService<OpenMediator.Abstractions.IMediator>();

var result = await mediator.Send(new GetUserQuery(10));
Console.WriteLine(result);
