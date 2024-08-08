using Application.Interfaces;
using AutoMapper;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Contracts.Product;
using KashewCheese.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Products.Queries.GetDetailProduct
{
    public class GetDetailProductQueryHandler : IRequestHandler<GetDetailProductQuery, GetProductDetailResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetDetailProductQueryHandler(ICacheService cacheService, IMapper mapper,IProductRepository productRepository)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<GetProductDetailResponse> Handle(GetDetailProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetDetailProduct(request.ProductId);

            return product;
        }
    }
}
