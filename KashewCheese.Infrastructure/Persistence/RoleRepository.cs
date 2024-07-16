using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using KashewCheese.Domain.Entities;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Infrastructure.Persistence
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddUserRole(List<RoleDto> roles, int userId)
        {
            foreach (RoleDto r in roles)
            {
                Role role = await GetRole(r.RoleName);

                UserRole userRole = new UserRole();
                userRole.UserId = userId;
                userRole.RoleId = role.Id;
                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role> GetRole(string name)
        {
            Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
            return role;
        }

        public async Task<List<Permission>> GetPermissionByEmail(string email)
        {
            var userRoles = await _context.Users
                .Where(u => u.Email == email)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role).ThenInclude(r=>r.RolePermissions).ThenInclude(rp=>rp.Permission).ToListAsync();

            List<Permission> permissions = userRoles
            .SelectMany(u => u.UserRoles)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission)
            .Distinct()
            .ToList();

            return permissions;

        }
    }
}
