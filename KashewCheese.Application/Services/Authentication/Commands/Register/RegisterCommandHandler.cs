using KashewCheese.Application.Common.Errors;
using KashewCheese.Application.Common.Interfaces.Authentication;
using KashewCheese.Application.Common.Interfaces.Email;
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
        private readonly IEmailService _emailService;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,IEmailService emailService)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
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
                UserRoles = command.Roles,
                IsEmailConfirmed=false,
                EmailVerificationCode= _emailService.GenerateVerificationCode(),
            };
            
            await _userRepository.Add(user);
            await _emailService.SendEmailAsync(user.Email, "Email Verification", $"Your verification code is {user.EmailVerificationCode}");


            return new AuthenticationResult(null,null,"Create users successfully, please verify email!");
        }
    }
}
