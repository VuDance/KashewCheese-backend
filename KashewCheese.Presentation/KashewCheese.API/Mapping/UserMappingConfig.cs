using AutoMapper;
using KashewCheese.Application.Authentication.Common;
using KashewCheese.Contracts.Users;

namespace KashewCheese.API.Mapping
{
    public class UserMappingConfig:Profile
    {
        public UserMappingConfig()
        {
            CreateMap<UserResult, UserListResponse>()
            .ForMember(dest => dest.UserResponses, opt => opt.MapFrom(src => src.Users));
        }
    }
}
