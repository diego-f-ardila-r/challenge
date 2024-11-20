using AutoMapper;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Entity;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Account.Queries;
using Metafar.Challenge.UseCase.Account.Queries.GetAccountInformationByCardNumber;
using Metafar.Challenge.UseCase.Automapper;
using Moq;

namespace Metafar.Challenge.UnitTest.UseCases;

public class GetAccountInformationByCardNumberHandlerTests
{
    private readonly Mock<IAccountQueryRepository> _accountQueryRepositoryMock;
    private readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ChallengeMapperProfile>()).CreateMapper();
    private readonly GetAccountInformationByCardNumberHandler _handler;
    private readonly ResponseModel<AccountUserDto> _response;

    public GetAccountInformationByCardNumberHandlerTests()
    {
        _accountQueryRepositoryMock = new Mock<IAccountQueryRepository>();
        _response = new ResponseModel<AccountUserDto>();
        _handler = new GetAccountInformationByCardNumberHandler(_response, _accountQueryRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldThrowNoContentException_WhenAccountNotFound()
    {
        // Arrange
        var request = new GetAccountInfoByCardNumberQuery { CardNumber = 1234567890 };
        _accountQueryRepositoryMock.Setup(repo => repo.GetAccountByCardNumberAsync(It.IsAny<int>())).ReturnsAsync(value: null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NoContentException>(() => _handler.Handle(request, CancellationToken.None));
        Assert.Equal("ACCOUNT_NOT_FOUND", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldReturnAccountUserDto_WhenAccountIsFound()
    {
        // Arrange
        var request = new GetAccountInfoByCardNumberQuery { CardNumber = 1234567890 };
        // generate data dummy for AccountEntity
        var account = new AccountEntity
        {
            AccountId = new Guid(),
            AccountNumber = 1002,
            Balance = 516.38,
            UserId = new Guid(),
            User = new UserEntity() { UserName = "janesmith", FirstName = "Jane", LastName = "Smith" }
        };
        account.Operations.Add(new OperationEntity(){ Amount = 516.38, CreatedDate = DateTime.Parse("2024-11-12T00:26:24.17") });
            
        _accountQueryRepositoryMock.Setup(repo => repo.GetAccountByCardNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result.Data);
    }
}