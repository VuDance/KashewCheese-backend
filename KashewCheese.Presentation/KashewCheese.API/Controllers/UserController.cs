using AutoMapper;
using KashewCheese.API.Attributes;
using KashewCheese.Application.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KashewCheese.Application.Services.Users.Queries.GetUserList;
using KashewCheese.Contracts.Users;
using KashewCheese.API.Common;

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
        [Route("GetUser")]
        [AuthorizePermission(UserPermission.ViewUser)]
        public async Task<IActionResult> GetAllUser([FromQuery] int page = 1, [FromQuery] int pageSize=10)
        {
            var users = await _mediator.Send(new GetUserListQuery(page,pageSize));
            var response = _mapper.Map<UserListResponse>(users);
            return Ok(response);

        }

        

    }
}
