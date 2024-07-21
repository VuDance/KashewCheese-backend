using KashewCheese.Application.Common.Interfaces.File;
using KashewCheese.Application.Services.Files.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Files.Commands.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadImageResult>
    {
        private readonly IFileStorageService _fileStorageService;
        public UploadImageCommandHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        public async Task<UploadImageResult> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var url = await _fileStorageService.UploadFileAsync(request.File.FileStream, request.File.FileName, request.File.ContentType,null);
            return new UploadImageResult(url);
        }
    }
}
