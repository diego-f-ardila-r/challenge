using Metafar.Challenge.Dto;
using Metafar.Challenge.Model;


namespace Metafar.Challenge.WebApp.Services;
public class MetafarServices(HttpClient httpClient)
{
    
    public async Task<ResponseModel<TokenDto>?> GetTokenAsync(int cardNumber, int pin)
    {
        var response = await httpClient.GetAsync($"v1/security/{cardNumber}/{pin}");
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadFromJsonAsync<ResponseModel<TokenDto>>();
        return token;
    }
    
    public async Task<ResponseModel<AccountUserDto>?> GetAccountInfoByCardAsync(int cardNumber)
    {
        var response = await httpClient.GetAsync($"v1/accounts/{cardNumber}");
        response.EnsureSuccessStatusCode();
        var accountInfo = await response.Content.ReadFromJsonAsync<ResponseModel<AccountUserDto>>();
        return accountInfo;
    }
    
    public async Task<ResponseModel<WithdrawDto>?> WithdrawFromAccountAsync(string cardNumber, double amount)
    {
        var withdraw = new
        {
            CardNumber = cardNumber,
            Amount = amount
        };
        var response = await httpClient.PostAsJsonAsync("v1/accounts/balance/withdraw", withdraw);
        response.EnsureSuccessStatusCode();
        var withdrawResponse = await response.Content.ReadFromJsonAsync<ResponseModel<WithdrawDto>>();
        return withdrawResponse;
    }
    
    public async Task<ResponseModel<IEnumerable<OperationDto>>?> GetOperationsAsync(int cardNumber, int page, int pageSize)
    {
        var response = await httpClient.GetAsync($"v1/operations/{cardNumber}?pageNumber={page}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();
        var operations = await response.Content.ReadFromJsonAsync<ResponseModel<IEnumerable<OperationDto>>>();
        return operations;
    }
}