using KashewCheese.Application.Common.Interfaces.ElasticSearch;
using KashewCheese.Contracts.ElasticSearch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Search.Queries.GetSearchResult
{
    public class GetSearchResultQueryHandler : IRequestHandler<GetSearchResultQuery, string>
    {
        private readonly IElasticSearchService<ElasticSearchRequest> _elasticSearchService;

        public GetSearchResultQueryHandler(IElasticSearchService<ElasticSearchRequest> elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public async Task<string> Handle(GetSearchResultQuery request, CancellationToken cancellationToken)
        {
            var newClass = new ElasticSearchRequest(Name:"ABABABABAB");
            var res= await _elasticSearchService.CreateDocumentAsync(newClass);
            return res;
        }
    }
}
