using KashewCheese.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Common.Interfaces.File
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string sizeLabel);
        Task<string> UploadFilesAsync(ICollection<FileUploadDto> fileUploadDtos);
    }
}
