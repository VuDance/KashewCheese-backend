using AutoMapper;
using KashewCheese.Application.Services.Products.Commands.CreateProduct;
using KashewCheese.Contracts.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashewCheese.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ProductController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
        {
            var command= _mapper.Map<CreateProductCommand>(createProductRequest);
            var result= await _mediator.Send(command);

            return Ok(new CreateProductResponse(result));
        }
    }
}
