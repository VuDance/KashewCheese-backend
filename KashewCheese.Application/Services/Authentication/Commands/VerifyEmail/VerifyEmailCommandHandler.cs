using KashewCheese.Application.Common.Interfaces.Persistence;
using MediatR;

namespace KashewCheese.Application.Services.Authentication.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, string>
    {
        private readonly IUserRepository _userRepository;
        public VerifyEmailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.VerifyEmailAsync(request.email, request.code);
            if (result)
            {
                return "Verify email successfully";
            }
            else
            {
                throw new NotImplementedException("Email or verify code is invalid");
            }
        }
    }
}
