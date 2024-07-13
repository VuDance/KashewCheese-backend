using AutoMapper;
using KashewCheese.API.Common;
using KashewCheese.Application.Services.Authentication.Commands.Register;
using KashewCheese.Application.Services.Authentication.Queries.Login;
using KashewCheese.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashewCheese.API.Controllers
{
    
    public class AuthenticationController : ApiControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var authResult = await _mediator.Send(command);

            var response = _mapper.Map<AuthenticationResponse>(authResult);

            return Ok(response);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(query);

            var response = _mapper.Map<AuthenticationResponse>(authResult);

            return Ok(response);
        }
    }
}
