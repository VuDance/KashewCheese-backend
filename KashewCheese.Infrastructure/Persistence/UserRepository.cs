using Application.Interfaces;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using KashewCheese.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KashewCheese.Infrastructure.Persistence
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoleRepository _roleRepository;

        public UserRepository(ApplicationDbContext context, IRoleRepository roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }


        public async Task Add(UserDto user)
        {
            User userCreated = new User();
            userCreated.FirstName = user.FirstName;
            userCreated.LastName = user.LastName;
            userCreated.Email = user.Email;
            userCreated.Password= user.Password;
            userCreated.IsEmailConfirmed = user.IsEmailConfirmed;
            userCreated.EmailVerificationCode = user.EmailVerificationCode;
            
            var userData= await _context.Users.AddAsync(userCreated);
            await _context.SaveChangesAsync();
            await _roleRepository.AddUserRole(user.UserRoles,userData.Entity.Id);

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            User user = await _context.Users.Include(u => u.UserRoles).ThenInclude(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<bool> VerifyEmailAsync(string email, string code)
        {
            var user = await GetUserByEmail(email);
            if (user != null && user.EmailVerificationCode == code)
            {
                user.IsEmailConfirmed = true;
                user.EmailVerificationCode = null;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
