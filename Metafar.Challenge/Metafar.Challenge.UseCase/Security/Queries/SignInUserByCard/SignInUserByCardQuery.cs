using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Model;

namespace Metafar.Challenge.UseCase.Security.Queries.SignInUserByCard;
public record SignInUserByCardQuery : IRequest<ResponseModel<TokenDto>>
{
    /// <summary>
    /// Gets or sets the card number.
    /// </summary>
    public int CardNumber { get; set; }

    /// <summary>
    /// Gets or sets the PIN.
    /// </summary>
    public int Pin { get; set; }
}