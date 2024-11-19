using Metafar.Challenge.Dto;
using Metafar.Challenge.Entity;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Account.Commands;
using Metafar.Challenge.Repository.Account.Queries;
using Metafar.Challenge.Repository.Operation.Commands;
using Metafar.Challenge.UseCase.Account.Commands.WithdrawFromAccount;
using Moq;

namespace Metafar.Challenge.UnitTest.UseCases;

public class WithdrawFromAccountHandlerTests
{
    private readonly Mock<IAccountQueryRepository> _accountQueryRepositoryMock;
    private readonly Mock<IAccountCommandRepository> _accountCommandRepositoryMock;
    private readonly Mock<IOperationCommandRepository> _operationCommandRepositoryMock;
    private readonly WithdrawFromAccountHandler _handler;

    public WithdrawFromAccountHandlerTests()
    {
        _accountQueryRepositoryMock = new Mock<IAccountQueryRepository>();
        _accountCommandRepositoryMock = new Mock<IAccountCommandRepository>();
        _operationCommandRepositoryMock = new Mock<IOperationCommandRepository>();
        var response = new ResponseModel<WithdrawDto>();

        _handler = new WithdrawFromAccountHandler(
            response,
            _accountQueryRepositoryMock.Object,
            _accountCommandRepositoryMock.Object,
            _operationCommandRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_AccountNotFound_ThrowsNotImplementedException()
    {
        // Arrange
        var request = new WithdrawFromAccountCommand { CardNumber = 1234, Amount = 100 };
        _accountQueryRepositoryMock.Setup(x => x.GetAccountByCardNumberAsync(request.CardNumber))
            .ReturnsAsync(value: null);

        // Act & Assert
        await Assert.ThrowsAsync<NoContentException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InsufficientBalance_ThrowsFunctionalException()
    {
        // Arrange
        var request = new WithdrawFromAccountCommand { CardNumber = 1234, Amount = 100 };
        var account = new AccountEntity { AccountId = Guid.NewGuid() , Balance = 50 };
        _accountQueryRepositoryMock.Setup(x => x.GetAccountByCardNumberAsync(request.CardNumber))
            .ReturnsAsync(account);

        // Act & Assert
        var result = await Assert.ThrowsAsync<FunctionalException>(() => _handler.Handle(request, CancellationToken.None));
        Assert.Equal("INSUFFICIENT_FUNDS", result.Message);
    }

    [Fact]
    public async Task Handle_SuccessfulWithdrawal_ReturnsResponseModel()
    {
        // Arrange
        const int initialBalance = 19500;
        const int withdrawalAmount = 500;
        const int accountNumber = 5678;
        const int cardNumber = 1234;

        var request = new WithdrawFromAccountCommand { CardNumber = cardNumber, Amount = withdrawalAmount };
        var account = new AccountEntity { AccountId = Guid.NewGuid(), Balance = initialBalance, AccountNumber = accountNumber };
        var operationResult = new OperationEntity { OperationId = Guid.NewGuid(), Amount = withdrawalAmount, OperationType = "Withdrawal", CreatedDate = DateTime.UtcNow };

        _accountQueryRepositoryMock.Setup(x => x.GetAccountByCardNumberAsync(cardNumber))
            .ReturnsAsync(account);
        _operationCommandRepositoryMock.Setup(x => x.InsertOperationAsync(It.IsAny<OperationEntity>()))
            .ReturnsAsync(operationResult);
        _accountCommandRepositoryMock.Setup(x => x.UpdateAccountBalanceAsync(It.IsAny<AccountEntity>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(account.AccountId, result.Data.AccountId);
        Assert.Equal(account.AccountNumber, result.Data.AccountNumber);
        Assert.Equal(initialBalance - withdrawalAmount, result.Data.Balance);
        Assert.Equal(cardNumber, result.Data.CardNumber);
        Assert.Equal(operationResult.OperationId, result.Data.OperationId);
        Assert.Equal(operationResult.OperationType, result.Data.OperationType);
        Assert.Equal(operationResult.Amount, result.Data.OperationAmount);
        Assert.Equal(operationResult.CreatedDate, result.Data.OperationDate);
    }
}