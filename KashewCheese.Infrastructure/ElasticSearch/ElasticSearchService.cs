using KashewCheese.Application.Common.Interfaces.ElasticSearch;
using KashewCheese.Contracts.ElasticSearch;
using Nest;

namespace KashewCheese.Infrastructure.ElasticSearch
{
    public class ElasticSearchService<T> : IElasticSearchService<T> where T : class
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<string> CreateDocumentAsync(T document,string id)
        {
            var response = await _elasticClient.IndexAsync(document,i=>i.Id(id));
            return response.IsValid ? "ok" : "ko";
        }

        public async Task<IList<SearchProductResponse>> SearchProduct(string keyword)
        {
            var searchResponse = await _elasticClient.SearchAsync<SearchProductResponse>(s => s
                .Source(src => src
                    .Includes(f => f
                        .Fields(
                            p => p.Name,
                            p => p.Slug
                        )
                    )
                )
                .Query(q => q
                    .Wildcard(m => m
                         .Field(f => f.Name)
                         .Value($"*{keyword}*")
                    )
                )
            );

            return searchResponse.Documents.ToList();
        }

        public Task<string> DeleteDocumentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllDocument(int pageIndex,int pageSize)
        {
            var from = (pageIndex - 1) * pageSize;
            var searchResponse = await _elasticClient.SearchAsync<T>(s => s
                .Query(q => q.MatchAll())
                .From(from)
                .Size(pageSize)
            );

            if (!searchResponse.IsValid)
            {
                // Xử lý lỗi nếu có
                throw new Exception($"Lỗi khi lấy tài liệu: {searchResponse.ServerError.Error.Reason}");
            }

            return searchResponse.Documents;
        }

        public Task<T> GetDocumentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateDocumentAsync(T document)
        {
            throw new NotImplementedException();
        }

    }
}
