using AutoMapper;
using KashewCheese.Application.Services.Files.Common;
using KashewCheese.Contracts.Images;

namespace KashewCheese.API.Mapping
{
    public class ImageMappingConfig:Profile
    {
        public ImageMappingConfig()
        {
            CreateMap<UploadImageResult, ImageResponse>();
        }
    }
}
