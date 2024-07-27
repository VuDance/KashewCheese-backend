using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Product
{
    public record CreateProductRequest(
         string Name,
         string Slug,
         string Description,
         string ProductThumb,
         int CategoryId,
         ICollection<CreateProductVariantRequest> Options
        );
}
