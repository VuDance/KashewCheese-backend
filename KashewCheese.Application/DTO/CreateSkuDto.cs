using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.DTO
{
    public class CreateSkuDto
    {
        public string Name { get; set; }
        public string SkuIdx { get; set; }
        public string Slug { get; set; }
        public string ProductThumb { get; set; }
        public float ProductPrice { get; set; }
        public int Stock { get; set; }
        public Guid ProductId { get; set; }
    }
}
