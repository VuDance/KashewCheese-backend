using AutoMapper;
using KashewCheese.API.Attributes;
using KashewCheese.API.Common;
using KashewCheese.Application.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KashewCheese.Application.Services.Users.Queries.GetUserList;
using KashewCheese.Contracts.Authentication;
using KashewCheese.Contracts.Users;

namespace KashewCheese.API.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public UserController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        [AuthorizePermission(UserPermission.ViewUser)]
        public async Task<IActionResult> GetAllUser()
        {
            var userList = await _mediator.Send(new GetUserListQuery());
            var response = _mapper.Map<UserListResponse>(userList);
            return Ok(response);

        }


    }
}
