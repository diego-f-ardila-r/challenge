namespace Metafar.Challenge.WebApp.ViewModel;

public record BaseViewModel
{
    public bool Successful { get; set; }
    public string? MessageCode { get; set; }
}