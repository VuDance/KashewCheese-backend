using Application.Interfaces;
using AutoMapper;
using Azure;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.Constants;
using KashewCheese.Application.DTO;
using KashewCheese.Contracts.Product;
using KashewCheese.Contracts.Roles;
using KashewCheese.Contracts.Users;
using KashewCheese.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence.Context;

namespace KashewCheese.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context,ICacheService cacheService , IMapper mapper)
        {
            _context = context;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<Guid> CreateProduct(CreateProductDto createProductDto)
        {
            Product product = new()
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Slug = createProductDto.Slug,
                ProductThumb = createProductDto.ProductThumb,
                CategoryId = createProductDto.CategoryId,
                ProductVariants = JsonConvert.SerializeObject(createProductDto.Options)
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var key = _cacheService.GenerateCacheKey(KeyPrefix.Product + product.Id, null, null);
            await _cacheService.SetCacheAsync(key,product,TimeSpan.FromDays(1));
            return product.Id;
        }

        public async Task<GetProductDetailResponse> GetDetailProduct(Guid productId)
        {
            Product product = await _context.Products.Include(p => p.Skus).FirstOrDefaultAsync(p => p.Id == productId);
            var key = _cacheService.GenerateCacheKey(KeyPrefix.Product+productId, null, null);
            List<ProductVariant> productVariants = JsonConvert.DeserializeObject<List<ProductVariant>>(product.ProductVariants);
            GetProductDetailResponse getProductDetailResponse = _mapper.Map<GetProductDetailResponse>(product);
            getProductDetailResponse.ProductVariants = productVariants;
            await _cacheService.SetCacheAsync(key, getProductDetailResponse, TimeSpan.FromDays(1));
            return getProductDetailResponse;
        }
    }
}
