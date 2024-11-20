namespace Metafar.Challenge.UseCase.Constants;

public record MessageCodeConstant
{
    public const string AccountNotFound = "ACCOUNT_NOT_FOUND";
    public const string InsufficientFunds = "INSUFFICIENT_FUNDS";
    public const string InvalidCardNumber = "INVALID_CARD_NUMBER";
    public const string InvalidOperationAmount = "INVALID_OPERATION_AMOUNT";
    public const string InvalidOperationType = "INVALID_OPERATION_TYPE";
    public const string OperationNotFound = "OPERATION_NOT_FOUND";
    public const string WithdrawSuccessful = "WITHDRAW_SUCCESSFUL";
    public const string WithdrawUnsuccessful = "WITHDRAW_UNSUCCESSFUL";
    public const string CardNotFound = "CARD_NOT_FOUND";
    public const string ValidationError = "VALIDATION_ERROR";
    public const string CardHasBeenBlocked = "CARD_HAS_BEEN_BLOCKED";
    public const string InvalidCardNumberOrPin = "INVALID_CARD_NUMBER_OR_PIN";
}
