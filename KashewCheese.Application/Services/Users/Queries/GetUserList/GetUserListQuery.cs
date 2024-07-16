using KashewCheese.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserResult>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetUserListQuery( int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
