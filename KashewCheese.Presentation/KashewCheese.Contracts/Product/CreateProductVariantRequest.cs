using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Product
{
    public class CreateProductVariantRequest
    {
        public string Image {  get; set; }
        public string Name { get; set; }

        public ICollection<string> Options { get; set; }
    }
}
