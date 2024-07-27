using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Categories
{
    public class CategoryDataResponse
    {
        public string Name {  get; set; }
        public string Slug { get; set; }
        public List<CategoryDataResponse>? SubCategories { get; set; }
    }
}
