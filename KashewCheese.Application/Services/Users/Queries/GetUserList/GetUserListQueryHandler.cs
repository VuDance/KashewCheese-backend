using Application.Interfaces;
using AutoMapper;
using KashewCheese.Application.Authentication.Common;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.Constants;
using KashewCheese.Contracts.Users;
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
        private readonly IMapper _mapper;
        public GetUserListQueryHandler(IUserRepository userRepository,ICacheService cacheService, IMapper mapper)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<UserResult> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await _userRepository.GetAll();
            var response = new UserResult(users);
            Dictionary<string, string> queryParamaters = new Dictionary<string, string>()
            {
                {
                    "page",request.Page.ToString()
                },
                {
                    "pageSize",request.PageSize.ToString()
                }
            };

            var key = _cacheService.GenerateCacheKey(KeyPrefix.User, queryParamaters, null);
            var dataCache= _mapper.Map<UserListResponse>(response);
            await _cacheService.SetCacheAsync(key,dataCache,TimeSpan.FromDays(1));
            return response;
        }
    }
}
