using KashewCheese.Application.Common.Interfaces.ElasticSearch;
using KashewCheese.Contracts.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Infrastructure.ElasticSearch
{
    public class ElasticSearchService<T> : IElasticSearchService<T> where T : class
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<string> CreateDocumentAsync(T document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            return response.IsValid ? "ok" : "ko";
        }

        public Task<string> DeleteDocumentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllDocument()
        {
            throw new NotImplementedException();
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
