using Metafar.Challenge.Dto;
using Metafar.Challenge.Entity;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Commands.Card;
using Metafar.Challenge.Repository.Queries.Card;
using Metafar.Challenge.UseCase.Security.Queries.SignInUserByCard;
using Microsoft.Extensions.Logging;
using Moq;

namespace Metafar.Challenge.UnitTest.UseCases;

public class SignInUserByCardHandlerTests
{
    private readonly Mock<ICardQueryRepository> _cardQueryRepositoryMock;
    private readonly SignInUserByCardHandler _tokenHandler;

    public SignInUserByCardHandlerTests()
    {
        _cardQueryRepositoryMock = new Mock<ICardQueryRepository>();
        Mock<ICardCommandRepository> cardCommandRepositoryMock = new();
        Mock<ILogger<SignInUserByCardHandler>> loggerMock = new();
        ResponseModel<TokenDto> response = new();
        _tokenHandler = new SignInUserByCardHandler(response, _cardQueryRepositoryMock.Object, cardCommandRepositoryMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task ShouldThrowNotFoundException_WhenCardWasNotFound()
    {
        // Arrange
        var request = new SignInUserByCardQuery { CardNumber = 1234567890, Pin = 1234 };
        _cardQueryRepositoryMock.Setup(repo => repo.GetCardByCardNumberAsync(It.IsAny<int>())).ReturnsAsync(value: null);

        // Act
        var result = await Assert.ThrowsAsync<NoContentException>(() => _tokenHandler.Handle(request, CancellationToken.None));
        
        // Assert
        Assert.Equal("CARD_NOT_FOUND", result.Message);
    }
    
    [Fact]
    public async Task ShouldThrowFunctionalException_WhenCardIsBlocked()
    {
        // Arrange
        var request = new SignInUserByCardQuery { CardNumber = 1234567890, Pin = 1234 };
        var card = new CardEntity() { IsBlocked = true };
        _cardQueryRepositoryMock.Setup(repo => repo.GetCardByCardNumberAsync(It.IsAny<int>())).ReturnsAsync(card);

        // Act
        var result = await Assert.ThrowsAsync<FunctionalException>(() => _tokenHandler.Handle(request, CancellationToken.None));
        
        // Assert
        Assert.Equal("THE_CARD_HAS_BEEN_BLOCKED", result.Message);
    }

    [Fact]
    public async Task ShouldThrowFunctionalException_WhenPinIsIncorrect()
    {
        // Arrange
        var request = new SignInUserByCardQuery { CardNumber = 1234567890, Pin = 1234};
        var card = new CardEntity { AccessPin = 0000, FailedAttempts = 0 };
        _cardQueryRepositoryMock.Setup(repo => repo.GetCardByCardNumberAsync(It.IsAny<int>())).ReturnsAsync(card);

        // Act
        var result =  await Assert.ThrowsAsync<FunctionalException>(() => _tokenHandler.Handle(request, CancellationToken.None));
        
        // Assert
        Assert.Equal("INVALID_CARD_OR_PIN", result.Message);
    }

    [Fact]
    public async Task ShouldBlockCard_WhenFailedAttemptsExceedLimit()
    {
        // Arrange
        var request = new SignInUserByCardQuery { CardNumber = 1234567890, Pin = 1234 };
        var card = new CardEntity { AccessPin = 0000, FailedAttempts = 5 };
        _cardQueryRepositoryMock.Setup(repo => repo.GetCardByCardNumberAsync(It.IsAny<int>())).ReturnsAsync(card);

        // Act
        var result = await Assert.ThrowsAsync<FunctionalException>(() => _tokenHandler.Handle(request, CancellationToken.None));

        // Assert
        Assert.Equal("THE_CARD_HAS_BEEN_BLOCKED", result.Message);
    }

    [Fact]
    public async Task ShouldReturnToken_WhenAuthenticationIsSuccessful()
    {
        // Arrange
        var request = new SignInUserByCardQuery { CardNumber = 1234567890, Pin = 1234 };
        var card = new CardEntity(){ CardNumber = 1234567890, AccessPin = 1234, FailedAttempts = 0 };
        
        _cardQueryRepositoryMock.Setup(repo => repo.GetCardByCardNumberAsync(It.IsAny<int>())).ReturnsAsync(card);
        
        // Act
        var result = await _tokenHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data.Token);
    }
}