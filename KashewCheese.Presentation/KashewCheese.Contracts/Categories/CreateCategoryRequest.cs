using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Categories
{
    public record CreateCategoryRequest(
     string Name,
     string DescriptionVN,
     string DescriptionEN,
     string Slug,
     bool? IsDelete, 
     bool? IsDraft ,
     bool? IsPublished,
     int? ParentCategoryId
    );
}
