using AutoMapper;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Products.Commands.CreateProduct;
using KashewCheese.Contracts.Product;
using KashewCheese.Domain.Entities;

namespace KashewCheese.API.Mapping
{
    public class ProductMappingConfig:Profile
    {
        public ProductMappingConfig()
        {
            CreateMap<CreateProductRequest,CreateProductCommand>();
            CreateMap<CreateProductCommand,CreateProductDto>();
            CreateMap<Product, GetProductsResponse>();
        }
    }
}
