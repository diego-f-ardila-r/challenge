using System.ComponentModel.DataAnnotations;
using Blazored.LocalStorage;
using Metafar.Challenge.WebApp.Services;

namespace Metafar.Challenge.WebApp.ViewModel;

public record LoginViewModel(MetafarService MetafarService, ILocalStorageService localStorage) : BaseViewModel
{
    [Required(ErrorMessage= "El number de tarjeta es requerido")]
    [Range(10000000, 99999999, ErrorMessage = "El número de tarjeta debe tener al menos 8 digitos")]
    public int CardNumber { get; set; }

    [Required]
    [Range(1000, 9999, ErrorMessage = "El pin debe tener al menos 4 digitos")]
    public int Pin { get; set; }
    
    public string Token { get; set; }
    
    public async Task<LoginViewModel> GenerateToken()
    {
        var response = await MetafarService.GetTokenAsync(CardNumber,Pin);
        Token = response?.Data?.Token;
        if (!string.IsNullOrEmpty(Token))
        {
            await localStorage.SetItemAsync("token", $"{Token}");
            Successful = true;
        }else{
            MessageCode = response?.MessageCode;
            Successful = false;
        }
        return this;
    }
}