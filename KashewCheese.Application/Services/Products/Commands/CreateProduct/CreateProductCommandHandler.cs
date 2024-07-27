using AutoMapper;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            CreateProductDto createProductDto=_mapper.Map<CreateProductDto>(request);
            await _productRepository.CreateProduct(createProductDto);
            return "Create successfully";
        }
    }
}
