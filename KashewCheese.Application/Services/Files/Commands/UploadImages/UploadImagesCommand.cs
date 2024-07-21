using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Files.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Files.Commands.UploadImages
{
    public record UploadImagesCommand(ICollection<FileUploadDto> Files):IRequest<UploadImagesResult>;
}
