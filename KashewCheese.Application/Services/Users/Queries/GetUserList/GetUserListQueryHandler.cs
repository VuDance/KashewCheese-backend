using Application.Interfaces;
using KashewCheese.Application.Authentication.Common;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
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
        private readonly ICacheService _cacheService;
        public GetUserListQueryHandler(IUserRepository userRepository,ICacheService cacheService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
        }
        public async Task<UserResult> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await _userRepository.GetAll();
            return new UserResult(users);
        }
    }
}
