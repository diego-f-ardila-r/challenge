using System.ComponentModel;
using System.Runtime.CompilerServices;
using Metafar.Challenge.WebApp.Services;

namespace Metafar.Challenge.WebApp.ViewModel;

public class AccountViewModel(MetafarServices metafarService) : INotifyPropertyChanged
{
    public string? AccountNumber { get; set; }
    public double Balance { get; set; }
    public DateTime LastWithdrawalDate { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }

    private List<OperationViewModel> _operations = new();
    public IEnumerable<OperationViewModel> Operations
    {
        get => _operations;
        set
        {
            _operations = value.ToList();
            OnPropertyChanged();
        }
    }

    public async Task<AccountViewModel> GetAccountViewModel(int cardNumber)
    {
        var response = await metafarService.GetAccountInfoByCardAsync(cardNumber);
        var account = response?.Data;

        if (account != null)
        {
            AccountNumber = account.AccountNumber;
            Balance = account.Balance;
            LastWithdrawalDate = account.LastWithdrawalDate;
            UserName = account.UserName;
            FullName = account.FullName;
        }

        return this;
    }

    public async Task GetOperations(int cardNumber, int page, int pageSize)
    {
        var result = await metafarService.GetOperationsAsync(cardNumber, page, pageSize);

        if (result?.Data != null)
        {
            Operations = result.Data.Select(operation => new OperationViewModel
            {
                OperationId = operation.OperationId,
                AccountId = operation.AccountId,
                OperationType = operation.OperationType,
                Amount = operation.Amount,
                CreatedDate = operation.CreatedDate
            }).ToList();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}