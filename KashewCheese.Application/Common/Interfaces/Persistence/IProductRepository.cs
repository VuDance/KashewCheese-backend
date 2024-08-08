using KashewCheese.Application.DTO;
using KashewCheese.Contracts.Product;


namespace KashewCheese.Application.Common.Interfaces.Persistence
{
    public interface IProductRepository
    {
        Task<Guid> CreateProduct(CreateProductDto createProductDto);
        Task<GetProductDetailResponse> GetDetailProduct(Guid productId);
    }
}
