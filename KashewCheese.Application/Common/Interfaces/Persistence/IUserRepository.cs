using KashewCheese.Application.DTO;
using KashewCheese.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);

        Task Add(UserDto user);

        Task<List<User>> GetAll();

        Task<bool> VerifyEmailAsync(string email, string code);
    }
}
