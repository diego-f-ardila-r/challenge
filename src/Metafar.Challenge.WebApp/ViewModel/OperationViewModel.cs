using Metafar.Challenge.WebApp.Services;

namespace Metafar.Challenge.WebApp.ViewModel;

public class OperationViewModel()
{
    public Guid OperationId { get; set; }

    public Guid AccountId { get; set; }
    
    public string OperationType { get; set; }

    public double Amount { get; set; }
    
    public DateTime CreatedDate { get; set; }
}