using KashewCheese.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.Categories
{
    public record GetCategoriesResponse
    {
        public List<CategoryDataResponse> Categories { get; set; }
        public GetCategoriesResponse()
        {
            Categories = new List<CategoryDataResponse>();
        }
    }
}
