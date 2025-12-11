using OpenMediator.Abstractions.Pipeline;
using ConsoleSample.Queries;

namespace ConsoleSample.Validators;

public class GetUserValidator : IValidator<GetUserQuery>
{
    public void Validate(GetUserQuery instance)
    {
        if (instance.UserId <= 0)
            throw new ArgumentException("UserId must be greater than zero.");
    }
}
