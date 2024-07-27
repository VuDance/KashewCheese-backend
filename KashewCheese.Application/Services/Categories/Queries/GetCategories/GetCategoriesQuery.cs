using KashewCheese.Application.Services.Categories.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<GetCategoriesResult>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetCategoriesQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
