using AutoMapper;
using Metafar.Challenge.Dto;
using Metafar.Challenge.Entity;

namespace Metafar.Challenge.UseCase.Automapper;

public class ChallengeMapperProfile : Profile
{
    public ChallengeMapperProfile()
    {
        CreateMap<AccountEntity, AccountUserDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.FullName, opt
                => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            .ForMember(dest => dest.LastWithdrawalDate, opt => opt.MapFrom(src => src.Operations.FirstOrDefault().CreatedDate));
    }
}