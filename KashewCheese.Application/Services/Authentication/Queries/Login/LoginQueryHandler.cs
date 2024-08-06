using KashewCheese.Application.Common.Errors;
using KashewCheese.Application.Common.Interfaces.Authentication;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Authentication.Common;
using KashewCheese.Domain.Entities;
using MediatR;


namespace KashewCheese.Application.Services.Authentication.Queries.Login
{
    public class LoginQueryHandler :
    IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleRepository = roleRepository;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByEmail(query.Email);
            if (user == null)
            {
                throw new InvalidUser();

            }

            if (!BCrypt.Net.BCrypt.Verify(query.Password, user.Password))
            {
                throw new InvalidPassword();
            }

            UserDto userDto = new UserDto();
            userDto.Email = user.Email;
            List<RoleDto> roles = new List<RoleDto>();
            foreach (UserRole userRole in user.UserRoles)
            {
                RoleDto roleDto = new RoleDto();
                roleDto.RoleName = userRole.Role.Name;
                roles.Add(roleDto);
            }
            userDto.UserRoles = roles;


            var token = _jwtTokenGenerator.GenerateToken(userDto);

            return new AuthenticationResult(
                userDto,
                token,"Login successfully");
        }
    }
}
