using AutoMapper;
using KashewCheese.API.Common;
using KashewCheese.Application.Services.Products.Commands.CreateProduct;
using KashewCheese.Application.Services.Products.Queries.GetDetailProduct;
using KashewCheese.Application.Services.Products.Queries.GetProducts;
using KashewCheese.Application.Services.Users.Queries.GetUserList;
using KashewCheese.Contracts.Product;
using KashewCheese.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashewCheese.API.Controllers
{
    public class ProductController : ApiControllerBase
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
        [HttpGet]
        [AllowAnonymous]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _mediator.Send(new GetProductsQuery(page, pageSize));
            return Ok(res);

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("GetDetailProduct/{id}")]
        public async Task<IActionResult> GetDetailProduct(Guid id)
        {
            var res = await _mediator.Send(new GetDetailProductQuery(id));
            return Ok(res);
        }
    }
}
