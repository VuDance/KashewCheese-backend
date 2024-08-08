using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KashewCheese.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ProductThumb { get; set; }
        public string? ProductAttributes { get; set; }
        public string ProductVariants {  get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        public bool IsPublished { get; set; } = true;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Sku> Skus { get; set; }

    }
}
