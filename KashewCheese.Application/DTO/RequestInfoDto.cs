using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.DTO
{
    public class RequestInfoDto
    {
        public string Path { get; set; }
        public IDictionary<string, string> QueryParameters { get; set; }
        public string? Claims { get; set; }
    }
}
