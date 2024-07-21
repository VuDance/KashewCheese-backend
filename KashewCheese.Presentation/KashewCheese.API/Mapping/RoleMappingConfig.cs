using AutoMapper;
using KashewCheese.Contracts.Roles;
using KashewCheese.Domain.Entities;

namespace KashewCheese.API.Mapping
{
    public class RoleMappingConfig:Profile
    {
        public RoleMappingConfig() 
        {
            CreateMap<Permission, PermissionResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.Name));
            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(r => r.RolePermissions.Select(rp => rp.Permission).ToList()));
        }
    
    }
}
