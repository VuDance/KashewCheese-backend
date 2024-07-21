using KashewCheese.Application.Services.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Authentication.Queries.Login
{
    public record LoginQuery(
    string Email,
    string Password) : IRequest<AuthenticationResult>;
}
