using KashewCheese.Application.Common.Interfaces.ElasticSearch;
using KashewCheese.Contracts.ElasticSearch;
using MediatR;
using Nest;

namespace KashewCheese.Application.Services.Search.Queries.GetSearchResult
{
    public class GetSearchResultQueryHandler : IRequestHandler<GetSearchResultQuery, IList<SearchProductResponse>>
    {
        private readonly IElasticSearchService<ElasticSearchRequest> _elasticSearchService;

        public GetSearchResultQueryHandler(IElasticSearchService<ElasticSearchRequest> elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public async Task<IList<SearchProductResponse>> Handle(GetSearchResultQuery request, CancellationToken cancellationToken)
        {
            var rq = new ElasticSearchRequest(request.Keyword);
            var res= await _elasticSearchService.SearchProduct(rq.Keyword);
            return res;
        }
    }
}
