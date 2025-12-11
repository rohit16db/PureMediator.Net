using PureMediator.Net.Abstractions.Requests;

namespace ConsoleSample.Queries;

public record GetUserQuery(int UserId) : IRequest<string>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, string>
{
    public Task<string> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => Task.FromResult($"User: {request.UserId}");
}
