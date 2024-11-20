using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Model;

namespace Metafar.Challenge.UseCase.Account.Commands.WithdrawFromAccount;

public record WithdrawFromAccountCommand : IRequest<ResponseModel<WithdrawDto>>
{
    public int CardNumber { get; set; }
    public double Amount { get; set; }
}