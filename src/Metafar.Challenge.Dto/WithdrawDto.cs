namespace Metafar.Challenge.Dto;

public record WithdrawDto
{
    public Guid AccountId { get; set; }
    public int AccountNumber { get; set; }
    public double Balance { get; set; }
    public int CardNumber { get; set; }
    public Guid OperationId { get; set; }
    public string? OperationType { get; set; }
    public double OperationAmount { get; set; }
    public DateTime OperationDate { get; set; }
}