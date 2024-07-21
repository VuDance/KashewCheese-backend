using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.DTO
{
    public class FileUploadDto
    {
         public Stream FileStream { get; set; }
         public string FileName { get; set; }
         public string ContentType { get; set; }

    }
}
