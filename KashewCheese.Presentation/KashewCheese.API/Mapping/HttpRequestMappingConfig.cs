using AutoMapper;
using KashewCheese.Application.DTO;

namespace KashewCheese.API.Mapping
{
    public class HttpRequestMappingConfig:Profile
    {
        public HttpRequestMappingConfig()
        {
            CreateMap<HttpRequest, RequestInfoDto>()
                .ForMember(dest => dest.QueryParameters, opt => opt.MapFrom(src => src.Query.ToDictionary(q => q.Key, q => q.Value.ToString())));
        }
    }
}
