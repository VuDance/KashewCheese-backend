using AutoMapper;
using KashewCheese.Application.Authentication.Common;
using KashewCheese.Contracts.Users;
using KashewCheese.Domain.Entities;

namespace KashewCheese.API.Mapping
{
    public class UserMappingConfig:Profile
    {
        public UserMappingConfig()
        {
            CreateMap<User, UserResponse>(); // Ánh xạ từ User sang UserResponse

            CreateMap<UserResult, UserListResponse>()
                .ForMember(dest => dest.UserResponses, opt => opt.MapFrom(src => src.Users));
            CreateMap<List<User>, UserResult>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src));
        }
    }
}
