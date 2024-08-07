using Application.Interfaces;
using AutoMapper;
using KashewCheese.Application.Common.Interfaces.ElasticSearch;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.Services.Products.Common;
using KashewCheese.Contracts.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly IElasticSearchService<GetProductsResponse> _elasticSearchService;

        public GetProductsQueryHandler(ICacheService cacheService, IMapper mapper, IElasticSearchService<GetProductsResponse> elasticSearchService)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _elasticSearchService = elasticSearchService;
        }
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _elasticSearchService.GetAllDocument(request.Page,request.PageSize);
            var result = _mapper.Map<List<GetProductsResponse>>(productList);

            return new GetProductsResult(result);
        }
    }
}
