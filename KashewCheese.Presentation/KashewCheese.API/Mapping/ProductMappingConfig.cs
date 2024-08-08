using AutoMapper;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Products.Commands.CreateProduct;
using KashewCheese.Contracts.Product;
using KashewCheese.Domain.Entities;
using Newtonsoft.Json;

namespace KashewCheese.API.Mapping
{
    public class ProductMappingConfig:Profile
    {
        public ProductMappingConfig()
        {
            CreateMap<CreateProductRequest,CreateProductCommand>();
            CreateMap<CreateProductCommand,CreateProductDto>();
            CreateMap<Product, GetProductsResponse>();
            CreateMap<Product,GetProductDetailResponse>()
                .ForMember(dest => dest.ProductVariants, opt => opt.Ignore());
            CreateMap<Sku, SkusResponse>()
                .ForMember(dest => dest.SkuIdx, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<int>>(src.SkuIdx)));
        }
    }
}
