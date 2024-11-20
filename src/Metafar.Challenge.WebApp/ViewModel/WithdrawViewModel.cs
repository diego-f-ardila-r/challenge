using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Metafar.Challenge.WebApp.Services;

namespace Metafar.Challenge.WebApp.ViewModel;

public record WithdrawViewModel(MetafarService metafarService) : BaseViewModel
{
    public Guid AccountId { get; set; }
    public int AccountNumber { get; set; }
    public double Balance { get; set; }
    public int CardNumber { get; set; }
    public Guid OperationId { get; set; }
    public string? OperationType { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto de la operación debe ser mayor a 0")]
    public double OperationAmount { get; set; }
    public DateTime OperationDate { get; set; }
    
    public async Task<WithdrawViewModel> WithdrawFromAccountAsync(int cardNumber, double amount)
    {
        var response = await metafarService.WithdrawFromAccountAsync(cardNumber, amount);
        
        if (response?.Data != null)
        {
            var withdraw = response?.Data;
            
            AccountNumber = withdraw.AccountNumber;
            Balance = withdraw.Balance;
            CardNumber = withdraw.CardNumber;
            OperationId = withdraw.OperationId;
            OperationType = withdraw.OperationType;
            OperationAmount = withdraw.OperationAmount;
            OperationDate = withdraw.OperationDate;
            Successful = true;
        }
        else
        {
            MessageCode = response?.MessageCode;
            Successful = false;
        }

        return this;
    }
}