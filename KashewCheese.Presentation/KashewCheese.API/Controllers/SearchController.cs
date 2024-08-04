using KashewCheese.API.Common;
using KashewCheese.Application.Services.Search.Queries.GetSearchResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashewCheese.API.Controllers
{
    public class SearchController : ApiControllerBase
    {
        private readonly ISender _mediator;
        public SearchController(ISender mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("SearchProduct")]
        public async Task<IActionResult> GetSearchResult([FromQuery] string keyword)
        {
            var res = await _mediator.Send(new GetSearchResultQuery(keyword));
            return Ok(res);
        }
    }
}
