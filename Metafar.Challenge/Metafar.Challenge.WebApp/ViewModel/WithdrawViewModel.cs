using System.ComponentModel;
using Metafar.Challenge.WebApp.Services;

namespace Metafar.Challenge.WebApp.ViewModel;

public class WithdrawViewModel(MetafarServices metafarService)
{
    public Guid AccountId { get; set; }
    public int AccountNumber { get; set; }
    public double Balance { get; set; }
    public int CardNumber { get; set; }
    public Guid OperationId { get; set; }
    public string? OperationType { get; set; }
    public double OperationAmount { get; set; }
    public DateTime OperationDate { get; set; }
    
    public async Task<WithdrawViewModel> WithdrawFromAccountAsync(int cardNumber, double amount)
    {
        var response = await metafarService.WithdrawFromAccountAsync(cardNumber, amount);
        var withdraw = response?.Data;

        if (withdraw != null)
        {
            AccountNumber = withdraw.AccountNumber;
            Balance = withdraw.Balance;
            CardNumber = withdraw.CardNumber;
            OperationId = withdraw.OperationId;
            OperationType = withdraw.OperationType;
            OperationAmount = withdraw.OperationAmount;
            OperationDate = withdraw.OperationDate;
        }

        return this;
    }
}