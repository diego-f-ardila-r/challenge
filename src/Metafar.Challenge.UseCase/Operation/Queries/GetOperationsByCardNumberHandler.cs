using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Repository.Operation.Queries;
using Metafar.Challenge.Infrastructure.Exceptions;
using Metafar.Challenge.Model;
using Metafar.Challenge.Repository.Queries.Card;
using Metafar.Challenge.UseCase.Constants;

namespace Metafar.Challenge.UseCase.Operation.Queries;

/// <summary>
/// Handler for retrieving operations by card number.
/// </summary>
public class GetOperationsByCardNumberHandler(
    ResponseModel<IEnumerable<OperationDto>> response,
    IOperationQueryRepository operationQueryRepository,
    ICardQueryRepository cardQueryRepository,
    IValidator<GetOperationsByCardNumberQuery> validator,
    IMapper mapper
) : IRequestHandler<GetOperationsByCardNumberQuery, ResponseModel<IEnumerable<OperationDto>>> 
{
    public async Task<ResponseModel<IEnumerable<OperationDto>>> Handle(GetOperationsByCardNumberQuery request, CancellationToken cancellationToken)
    {
        // validate the request
        var resultValidator = await validator.ValidateAsync(request, cancellationToken);
        
        if (!resultValidator.IsValid) throw new ValidatorException(MessageCodeConstant.ValidationError, resultValidator.Errors);

        // validate if the card number exists
        var accountInformation = await cardQueryRepository.GetCardByNumberAsync(request.CardNumber);
        if (accountInformation == null) throw new NoContentException(MessageCodeConstant.CardNotFound);
        
        // retrieve the operations by card number
        var operations = await operationQueryRepository.GetOperationsByCardNumberAsync(request.CardNumber, request.PageNumber, request.PageSize);
        
        // validate if there are operations
        if (!operations.Operations.Any()) throw new NoContentException(MessageCodeConstant.OperationNotFound);
        
        // map the operations to DTOs
        response.Data = mapper.Map<IEnumerable<OperationDto>>(operations.Operations);
        
        // Map pagination metadata
        response.Metadata = new
        {
            Pagination = new
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = operations.TotalPages
            }
        };
        
        return response;
    }
}
