using MediatR;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Model;

namespace Metafar.Challenge.UseCase.Security.Queries.Authentication;
public class AuthenticationQuery : IRequest<ResponseModel<TokenDto>>
{
    public int CardNumber { get; set; }
    public int Pin { get; set; }
}