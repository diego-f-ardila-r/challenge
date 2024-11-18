using FluentValidation;

namespace Metafar.Challenge.UseCase.Operation.Queries;

public class GetOperationsByCardNumberQueryValidator : AbstractValidator<GetOperationsByCardNumberQuery>
{
    public GetOperationsByCardNumberQueryValidator()
    {
        RuleFor(x => x.CardNumber)
            .GreaterThan(0).WithMessage("Card number must be greater than 0.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(10).WithMessage("Page size must be 10 or less.");
    }
}