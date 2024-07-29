using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Product
{
    public record CreateProductSkuRequest(
        string Name,
        ICollection<string> SkuIdx,
        string Slug,
        string ProductThumb,
        float ProductPrice,
        int Stock,
        Guid ProductId
     );
}
