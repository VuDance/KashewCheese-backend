using AutoMapper;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Authentication.Commands.Register;
using KashewCheese.Application.Services.Authentication.Common;
using KashewCheese.Application.Services.Authentication.Queries.Login;
using KashewCheese.Contracts.Authentication;

namespace KashewCheese.API.Mapping
{
    public class AuthenticationMappingConfig : Profile
    {
        public AuthenticationMappingConfig()
        {
            CreateMap<AuthenticationResult, AuthenticationResponse>();
            CreateMap<LoginRequest, LoginQuery>();

            CreateMap<string, RoleDto>()
                .ConstructUsing(roleName => new RoleDto { RoleName = roleName });

            CreateMap<RegisterRequest, RegisterCommand>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));


        }
    }
}
