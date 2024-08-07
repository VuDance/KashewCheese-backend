using KashewCheese.Application.Services.Products.Common;
using MediatR;

namespace KashewCheese.Application.Services.Products.Queries.GetProducts
{
    public class GetProductsQuery:IRequest<GetProductsResult>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        
        public GetProductsQuery(int page, int pageSize) {
            Page = page;
            PageSize = pageSize;
        }
    }
}
