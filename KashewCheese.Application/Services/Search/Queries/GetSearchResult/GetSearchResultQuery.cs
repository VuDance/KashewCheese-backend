using KashewCheese.Contracts.ElasticSearch;
using MediatR;
using Nest;


namespace KashewCheese.Application.Services.Search.Queries.GetSearchResult
{
    public class GetSearchResultQuery : MediatR.IRequest<IList<SearchProductResponse>>
    {
        public string Keyword { get; set; }
        public GetSearchResultQuery(string keyword) {
            Keyword = keyword;
        }
    }
}
