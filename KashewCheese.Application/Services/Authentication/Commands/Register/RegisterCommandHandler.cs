using KashewCheese.Application.Common.Errors;
using KashewCheese.Application.Common.Interfaces.Authentication;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Authentication.Common;
using KashewCheese.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Authentication.Commands.Register
{
    public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            User userCheck = await _userRepository.GetUserByEmail(command.Email);
            if (userCheck!=null)
            {
                throw new DuplicateEmailException();
            }
            var user = new UserDto
            {
                Email = command.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                UserRoles = command.Roles
            };
            
            await _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
