using AutoMapper;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Categories.Commands.Create;
using KashewCheese.Application.Services.Categories.Common;
using KashewCheese.Contracts.Categories;

namespace KashewCheese.API.Mapping
{
    public class CategoryMappingConfig:Profile
    {
        public CategoryMappingConfig()
        {
            CreateMap<CreateCategoryRequest, CreateCommand>();
            CreateMap<CreateCommand,CategoryDto>();
            CreateMap<CreateCategoryResult,CategoryResponse>();
        }
    }
}
