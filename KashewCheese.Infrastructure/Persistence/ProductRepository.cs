using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using KashewCheese.Domain.Entities;
using Newtonsoft.Json;
using Persistence.Context;

namespace KashewCheese.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateProduct(CreateProductDto createProductDto)
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
        }
    }
}
