using Blazored.LocalStorage;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Model;


namespace Metafar.Challenge.WebApp.Services;
public class MetafarService(HttpClient httpClient, ILocalStorageService localStorage)
{
    private async  Task SetAuthorizationHeader()
    {
        var savedToken = await localStorage.GetItemAsync<string>("token");
        if (!string.IsNullOrEmpty(savedToken))
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {savedToken}");
        }
    }
    public async Task<ResponseModel<TokenDto>?> GetTokenAsync(int cardNumber, int pin)
    {
        var response = await httpClient.GetAsync($"v1/security/{cardNumber}/{pin}");
        var token = await response.Content.ReadFromJsonAsync<ResponseModel<TokenDto>>();
        return token;
    }
    
    public async Task<ResponseModel<AccountUserDto>?> GetAccountInfoByCardAsync(int cardNumber)
    {
        await SetAuthorizationHeader();
        var response = await httpClient.GetAsync($"v1/accounts/{cardNumber}");
        var accountInfo = await response.Content.ReadFromJsonAsync<ResponseModel<AccountUserDto>>();
        return accountInfo;
    }
    
    public async Task<ResponseModel<WithdrawDto>?> WithdrawFromAccountAsync(int cardNumber, double amount)
    {
        await SetAuthorizationHeader();
        var withdraw = new
        {
            CardNumber = cardNumber,
            Amount = amount
        };
        var response = await httpClient.PostAsJsonAsync("v1/accounts/balance/withdraw", withdraw);
        var withdrawResponse = await response.Content.ReadFromJsonAsync<ResponseModel<WithdrawDto>>();
        return withdrawResponse;
    }
    
    public async Task<ResponseModel<IEnumerable<OperationDto>>?> GetOperationsAsync(int cardNumber, int page, int pageSize)
    {
        await SetAuthorizationHeader();
        var response = await httpClient.GetAsync($"v1/operations/{cardNumber}?pageNumber={page}&pageSize={pageSize}");
        var operations = await response.Content.ReadFromJsonAsync<ResponseModel<IEnumerable<OperationDto>>>();
        return operations;
    }
}