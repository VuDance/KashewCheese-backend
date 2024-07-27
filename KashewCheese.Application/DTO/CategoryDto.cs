using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.DTO
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string DescriptionVN { get; set; }
        public string DescriptionEN { get; set; }
        public string Slug { get; set; }

        public bool IsDelete { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        public bool IsPublished { get; set; } = true;
        public int? ParentCategoryId {  get; set; }
    }
}
