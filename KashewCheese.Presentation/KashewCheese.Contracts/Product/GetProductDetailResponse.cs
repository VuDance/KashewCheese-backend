using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Product
{
    public class GetProductDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ProductThumb { get; set; }
        public object ProductAttributes { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDraft { get; set; }
        /*public bool IsPublished { get; set; }
        public int CategoryId { get; set; }
        public object Category { get; set; }
        public object Skus { get; set; }*/
        public List<SkusResponse> Skus { get; set; }
        public List<ProductVariant> ProductVariants { get; set; }
    }
    public class ProductVariant
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public List<string> Options { get; set; }
    }
    public class SkusResponse
    {
        public Guid Id { get; set; }
        public string Name { set; get; }
        public List<int> SkuIdx {  get; set; }
        public string ProductThumb { get; set; }
        public string ProductPrice { get; set; }
        public int Stock { get; set; }

    }
}
