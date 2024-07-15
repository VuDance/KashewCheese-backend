using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Users
{
    public record UserListResponse
    {
        public List<UserResponse> UserResponses { get; set; }
        public UserListResponse()
        {
            UserResponses = new List<UserResponse>();
        }
    };
}
