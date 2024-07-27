using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KashewCheese.Contracts.Categories;
using KashewCheese.Domain.Entities;

namespace KashewCheese.Application.Services.Categories.Common
{
    public record GetCategoriesResult
    {
        public List<Category> Categories { get; set; }
        public GetCategoriesResult()
        {
            Categories = new List<Category>();
        }
    }
}
