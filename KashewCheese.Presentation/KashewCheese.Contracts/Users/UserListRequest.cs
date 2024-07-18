using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Users
{
    public class UserListRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public UserListRequest(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
