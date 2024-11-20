using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Entity;
using Metafar.Challenge.Model;

namespace Metafar.Challenge.UseCase.Operation.Queries;

public record GetOperationsByCardNumberQuery : IRequest<ResponseModel<IEnumerable<OperationDto>>>
{
    /// <summary>
    /// Gets or sets the card number to filter operations.
    /// </summary>
    public int CardNumber { get; set; }

    /// <summary>
    /// Gets or sets the page number for pagination.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the page size for pagination.
    /// </summary>
    public int PageSize { get; set; }
}