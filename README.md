# PureMediator.Net

[![NuGet Version](https://img.shields.io/nuget/v/PureMediator.Net.svg)](https://www.nuget.org/packages/PureMediator.Net)
[![NuGet Downloads](https://img.shields.io/nuget/dt/PureMediator.Net.svg)](https://www.nuget.org/packages/PureMediator.Net)

**PureMediator.Net** is a clean, lightweight implementation of the Mediator Pattern for .NET.  
It focuses on **purity, simplicity, performance, and extensibility**, giving you a minimal yet powerful alternative to MediatR with full optional support for behaviors, validation, and automatic handler discovery.

---

# 🚀 Getting Started

This section includes everything you need to install, register, and use PureMediator.Net in your project.

---

## 1️⃣ Install the Package

Install via NuGet:

```bash
dotnet add package PureMediator.Net

```

## 2️⃣ Define a Request

Every request implements IRequest<TResponse> or IRequest (void response):
```csharp
public class PingRequest : IRequest<string>
{
    public string Message { get; set; } = "Hello";
}
```
### 3️⃣ Create a Handler

Each request must have a matching handler implementing 
```csharp
IRequestHandler<TRequest, TResponse>:
```

```csharp
public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Pong: {request.Message}");
    }
}
```
### 4️⃣ Register PureMediator.Net

Add this in your Program.cs or Startup:

````csharp
services.AddPureMediator();

````


This automatically registers:

All request handlers
All pipeline behaviors
All validators
All abstractions

### 5️⃣ Inject and Use IMediator

```csharp
public class DemoController : ControllerBase
{
    private readonly IMediator _mediator;

    public DemoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ping")]
    public async Task<string> Ping()
    {
        return await _mediator.Send(new PingRequest { Message = "Hello Mediator" });
    }
}
```

Output:

```console
Pong: Hello Mediator
```
## 🔄 Pipeline Behaviors (Optional)

Behaviors allow adding cross-cutting logic before and after the handler executes.

Example: Logging behavior
```csharp
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        Console.WriteLine($"➡ Handling {typeof(TRequest).Name}");

        var response = await next();

        Console.WriteLine($"⬅ Handled {typeof(TRequest).Name}");
        return response;
    }
}
```

Register it:
```csharp
services.AddPipelineBehavior(typeof(LoggingBehavior<,>));
```

## ✔ Validation Support (Optional)

You can add validation rules for any request.
```csharp 
//Create a Validator:

public class PingValidator : IRequestValidator<PingRequest>
{
    public void Validate(PingRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException("Message cannot be empty.");
    }
}
```
Enable validation:
```csharp
services.AddValidation();
```

If validation fails → request handler will NOT execute.

📂 Recommended Project Structure
src/
├── PureMediator.Net/
│   ├── Abstractions/
│   ├── Behaviors/
│   ├── Handlers/
│   ├── Validators/
│   ├── Extensions/
│   ├── PureMediator.Net.csproj
│   ├── README.md
│   ├── LICENSE

🤝 Contributing

## We welcome:

- Bug reports
- Feature
- suggestions
- Documentation 
- improvements
- Pull requests

### Workflow:
Fork the repo
Create a feature branch
Commit changes
Submit a PR

### 📄 License

This project is licensed under the MIT License.
See the LICENSE file for details.

### ⭐ Support

If this project helps you, please consider:

- ⭐ Starring the GitHub repository
- 📦 Using it in your projects
- 📣 Sharing it with the community

### 🔗 Useful Links

NuGet Package: [(PureMediator.Net)](https://www.nuget.org/packages/PureMediator.Net/)

GitHub Repository: ([PureMediator.Net](https://github.com/rohit16db/PureMediator.Net))