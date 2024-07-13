using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Authentication.Common;
using KashewCheese.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Authentication.Commands.Register
{
    public record RegisterCommand(
    string Email,
    string Password,
    List<RoleDto> Roles) : IRequest<AuthenticationResult>;
}
