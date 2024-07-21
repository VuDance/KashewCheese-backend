using KashewCheese.Application.Common.Interfaces.File;
using KashewCheese.Application.Services.Files.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Files.Commands.UploadImages
{
    public class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand, UploadImagesResult>
    {
        private readonly IFileStorageService _fileStorageService;

        public UploadImagesCommandHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        public async Task<UploadImagesResult> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
        {
            var res = await _fileStorageService.UploadFilesAsync(request.Files);
            return new UploadImagesResult(res);
        }
    }
}
