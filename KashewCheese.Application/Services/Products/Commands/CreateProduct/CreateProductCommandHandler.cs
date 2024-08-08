using Application.Interfaces;
using AutoMapper;
using KashewCheese.Application.Common.Interfaces.ElasticSearch;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using MediatR;

namespace KashewCheese.Application.Services.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISkuRepository _skuRepository;
        private readonly IMapper _mapper;
        private readonly IElasticSearchService<CreateProductDto> _elasticSearchService;


        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper,ISkuRepository skuRepository,IElasticSearchService<CreateProductDto> elasticSearchService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _skuRepository = skuRepository;
            _elasticSearchService = elasticSearchService;
        }
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            CreateProductDto createProductDto=_mapper.Map<CreateProductDto>(request);
            var productId= await _productRepository.CreateProduct(createProductDto);
            var skuDto = _mapper.Map<List<CreateSkuDto>>(createProductDto.Skus);
            foreach(var sku in skuDto)
            {
                sku.ProductId=productId;
            }
            await _skuRepository.CreateBulkSku(skuDto);
            await _elasticSearchService.CreateDocumentAsync(createProductDto, productId.ToString());

            return productId.ToString();
        }
    }
}
