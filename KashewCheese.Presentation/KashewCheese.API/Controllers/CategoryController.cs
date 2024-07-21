using AutoMapper;
using KashewCheese.API.Common;
using KashewCheese.Application.Services.Categories.Commands.Create;
using KashewCheese.Contracts.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashewCheese.API.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CategoryController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateCategoryRequest createCategoryRequest)
        {
            var command=_mapper.Map<CreateCommand>(createCategoryRequest);
            var result= await _mediator.Send(command);
            var response=_mapper.Map<CategoryResponse>(result);
            return Ok(response);
        }
    }
}
