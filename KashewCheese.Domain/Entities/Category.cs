using KashewCheese.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Domain.Entities
{
    [Table("Categories")]
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string DescriptionVN { get; set; }
        public string DescriptionEN { get; set; }
        public string Slug { get; set; }
        public int? ParentCategoryId {  get; set; }
        public Category? ParentCategory { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        public bool IsPublished { get; set; } = true;
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
