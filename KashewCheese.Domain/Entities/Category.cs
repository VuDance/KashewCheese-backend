using KashewCheese.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

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
