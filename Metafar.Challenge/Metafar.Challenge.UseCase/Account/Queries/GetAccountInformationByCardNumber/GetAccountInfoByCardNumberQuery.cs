using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Model;

namespace Metafar.Challenge.UseCase.Account.Queries.GetAccountInformationByCardNumber;

/// <summary>
/// Query to get account information by card number.
/// </summary>
public record GetAccountInfoByCardNumberQuery : IRequest<ResponseModel<AccountUserDto>>
{
    /// <summary>
    /// Gets or sets the card number.
    /// </summary>
    public int CardNumber { get; set; }
}