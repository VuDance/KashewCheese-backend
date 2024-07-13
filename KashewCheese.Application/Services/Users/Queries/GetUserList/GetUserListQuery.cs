using KashewCheese.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Users.Queries.GetUserList
{
    public record GetUserListQuery:IRequest<UserResult>;
}
