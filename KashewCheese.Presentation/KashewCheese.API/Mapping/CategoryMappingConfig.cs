using AutoMapper;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Categories.Commands.Create;
using KashewCheese.Application.Services.Categories.Common;
using KashewCheese.Contracts.Categories;
using KashewCheese.Domain.Entities;

namespace KashewCheese.API.Mapping
{
    public class CategoryMappingConfig:Profile
    {
        public CategoryMappingConfig()
        {
            CreateMap<CreateCategoryRequest, CreateCommand>();
            CreateMap<CreateCommand,CategoryDto>();
            CreateMap<CreateCategoryResult, CreateCategoryResponse>();
            CreateMap<List<Category>, GetCategoriesResult>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src));


            CreateMap<Category, CategoryDataResponse>()
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories ?? null));

            CreateMap<GetCategoriesResult, GetCategoriesResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
        }
    }
}
