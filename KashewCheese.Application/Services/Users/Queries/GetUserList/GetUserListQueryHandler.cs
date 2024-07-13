using KashewCheese.Application.Authentication.Common;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserResult>
    {
        private readonly IUserRepository _userRepository;
        public GetUserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResult> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await _userRepository.GetAll();
            return new UserResult(users);
        }
    }
}
