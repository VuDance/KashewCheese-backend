using KashewCheese.Application.Services.Search.Queries.GetSearchResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashewCheese.API.Controllers
{
    public class SearchController : ControllerBase
    {
        private readonly ISender _mediator;
        public SearchController(ISender mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("GetSearch")]
        public async Task<IActionResult> GetSearchResult()
        {
            var res = await _mediator.Send(new GetSearchResultQuery());
            return Ok(res);
        }
    }
}
