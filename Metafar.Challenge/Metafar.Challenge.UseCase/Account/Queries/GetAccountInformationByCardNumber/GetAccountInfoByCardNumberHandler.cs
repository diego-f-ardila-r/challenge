using AutoMapper;
using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Account.Queries;

namespace Metafar.Challenge.UseCase.Account.Queries.GetAccountInformationByCardNumber;

/// <summary>
/// Handler for the <see cref="GetAccountInfoQuery"/>.
/// </summary>
public class GetAccountInformationByCardNumberHandler(
    ResponseModel<AccountUserDto> response,
    IAccountQueryRepository accountQueryRepository,
    IMapper mapper
    ) : IRequestHandler<GetAccountInfoByCardNumberQuery, ResponseModel<AccountUserDto>>
{
    /// <summary>
    /// Handles the query to get account information by card number.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The account entity if found; otherwise, null.</returns>
    public async Task<ResponseModel<AccountUserDto>> Handle(GetAccountInfoByCardNumberQuery request, CancellationToken cancellationToken)
    {
        // retrieve the account by card number
        var account = await accountQueryRepository.GetAccountByCardNumberAsync(request.CardNumber);
        
        // map the account entity to a DTO
        response.Data = mapper.Map<AccountUserDto>(account);
        
        // validate if the account was found
        if (account == null)
            throw new NoContentException("ACCOUNT_NOT_FOUND");
        
        return response;
    }
}