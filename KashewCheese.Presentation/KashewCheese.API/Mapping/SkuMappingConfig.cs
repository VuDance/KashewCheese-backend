using AutoMapper;
using KashewCheese.Application.DTO;
using KashewCheese.Contracts.Product;
using KashewCheese.Domain.Entities;
using Newtonsoft.Json;

namespace KashewCheese.API.Mapping
{
    public class SkuMappingConfig:Profile
    {
        public SkuMappingConfig()
        {
            CreateMap<CreateProductSkuRequest, CreateSkuDto>()
                .ForMember(dest => dest.SkuIdx, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.SkuIdx)));
        }
    }
}
