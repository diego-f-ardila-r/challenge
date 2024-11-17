using Metafar.Challenge.Dto;
using Metafar.Challenge.Entity;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Account.Commands;
using Metafar.Challenge.Repository.Account.Queries;
using Metafar.Challenge.Repository.Operation.Commands;

namespace Metafar.Challenge.UseCase.Account.Commands.WithdrawFromAccount;

public class WithdrawFromAccountCommandHandler(
    ResponseModel<WithdrawDto> response,
    IAccountQueryRepository accountQueryRepository,
    IAccountCommandRepository accountCommandRepository,
    IOperationCommandRepository operationCommandRepository)
{
    public async Task<ResponseModel<WithdrawDto>>  HandleAsync(WithdrawFromAccountCommand request)
    {
        // Validate if there is an account using the given card number
        var account = await accountQueryRepository.GetAccountByCardNumberAsync(request.CardNumber);
        
        if (account == null)
        {
            throw new NotImplementedException("ACCOUNT_NOT_FOUND");
        }

        // Validate if the account has enough balance to withdraw
        if (account.Balance < request.Amount)
        {
            throw new FunctionalException("INSUFFICIENT_BALANCE");
        }

        // Insert the operation
        var operationResult = await operationCommandRepository.InsertOperationAsync(
                new OperationEntity
                {
                    AccountId = account.AccountId,
                    Amount = request.Amount,
                    OperationType = "Withdrawal"
                }
            );
        
        // Calculate the new balance
        account.Balance -= request.Amount;

        // Update the account balance
        await accountCommandRepository.UpdateAccountBalanceAsync(account);
        
        // set response
        response.Data = new WithdrawDto
        {
            AccountId = account.AccountId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            CardNumber = request.CardNumber,
            OperationId = operationResult.OperationId,
            OperationType = operationResult.OperationType,
            OperationAmount = operationResult.Amount,
            OperationDate = operationResult.CreatedDate
        };
        
        return response;
    }
}