using System.Transactions;
using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Entity;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Account.Commands;
using Metafar.Challenge.Repository.Account.Queries;
using Metafar.Challenge.Repository.Operation.Commands;
using Metafar.Challenge.UseCase.Constants;

namespace Metafar.Challenge.UseCase.Account.Commands.WithdrawFromAccount;

/// <summary>
/// Handler for withdrawing from an account.
/// </summary>
public class WithdrawFromAccountHandler(
    ResponseModel<WithdrawDto> response,
    IAccountQueryRepository accountQueryRepository,
    IAccountCommandRepository accountCommandRepository,
    IOperationCommandRepository operationCommandRepository) : IRequestHandler<WithdrawFromAccountCommand, ResponseModel<WithdrawDto>>
{
    public async Task<ResponseModel<WithdrawDto>>  Handle(WithdrawFromAccountCommand request, CancellationToken cancellationToken)
    {
        // Validate if there is an account using the given card number
        var account = await accountQueryRepository.GetAccountByCardNumberAsync(request.CardNumber);
        
        if (account == null)
        {
            throw new NoContentException(MessageCodeConstant.AccountNotFound);
        }

        // Validate if the account has enough balance to withdraw
        if (account.Balance < request.Amount)
        {
            throw new FunctionalException(MessageCodeConstant.InsufficientFunds);
        }

        // open a transaction
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        // Insert the operation
        var operationResult = await operationCommandRepository.InsertOperationAsync(
                new OperationEntity
                {
                    AccountId = account.AccountId,
                    Amount = request.Amount,
                    OperationType = "Withdrawal"
                }
            );
        
        // Set the new balance
        account.Balance -= request.Amount;

        // Update the account balance
        await accountCommandRepository.UpdateAccountBalanceAsync(account);
        
        // Commit the transaction
        scope.Complete();
        
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