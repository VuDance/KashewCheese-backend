using KashewCheese.Contracts.Product;
using MediatR;

namespace KashewCheese.Application.Services.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name,
         string Slug,
         string Description,
         string ProductThumb,
         int CategoryId,
         ICollection<CreateProductVariantRequest> Options,
         ICollection<CreateProductSkuRequest> Skus
         ) : IRequest<string>;
}
