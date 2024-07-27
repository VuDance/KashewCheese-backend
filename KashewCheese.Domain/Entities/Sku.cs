using System.ComponentModel.DataAnnotations;

namespace KashewCheese.Domain.Entities
{
    public class Sku
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SkuIdx { get; set; }
        public string Slug { get; set; }
        public string ProductThumb { get; set; }
        public float ProductPrice {  get; set; }
        public int Stock {  get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        public bool IsPublished { get; set; } = true;
        public Guid ProductId {  get; set; }
        public Product Product { get; set; }
    }
}
