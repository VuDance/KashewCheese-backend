using KashewCheese.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.DTO
{
    public class UserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<RoleDto>? UserRoles { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public string EmailVerificationCode { get; set; }
    }
}
