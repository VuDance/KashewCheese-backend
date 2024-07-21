using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Authentication.Common;
using MediatR;

namespace KashewCheese.Application.Services.Authentication.Commands.Register
{
    public record RegisterCommand(
    string Email,
    string Password,
    List<RoleDto> Roles) : IRequest<AuthenticationResult>;
}
