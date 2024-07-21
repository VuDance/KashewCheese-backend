using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Files.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Files.Commands.UploadImage
{
    public record UploadImageCommand(FileUploadDto File) :IRequest<UploadImageResult>;
}
