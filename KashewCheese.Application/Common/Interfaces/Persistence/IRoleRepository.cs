using KashewCheese.Application.DTO;
using KashewCheese.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Common.Interfaces.Persistence
{
    public interface IRoleRepository
    {
        Task AddUserRole(List<RoleDto> roles, int userId);
        Task<Role> GetRole(string name);

        Task<List<Role>> GetRoleByEmail(string email);

    }
}
