namespace Metafar.Challenge.UseCase.Security.Queries.SignInUserByCard;
using FluentValidation;

public class SignInUserByCardValidator : AbstractValidator<SignInUserByCardQuery>
{
    public SignInUserByCardValidator()
    {
        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number is required.")
            .GreaterThan(0).WithMessage("Card number must be a positive integer.");

        RuleFor(x => x.Pin)
            .NotEmpty().WithMessage("PIN is required.")
            .InclusiveBetween(1000, 9999).WithMessage("PIN must be a 4-digit number.");
    }
}