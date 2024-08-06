using KashewCheese.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashewCheese.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public string? EmailVerificationCode { get; set; }
    }
}
