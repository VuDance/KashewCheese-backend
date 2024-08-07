using KashewCheese.Contracts.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Common.Interfaces.ElasticSearch
{
    public interface IElasticSearchService<T>
    {
        Task<string> CreateDocumentAsync(T document,string id);
        Task<T> GetDocumentAsync(int id);
        Task<IEnumerable<T>> GetAllDocument(int pageIndex,int pageSize);
        Task<string> UpdateDocumentAsync(T document);
        Task<string> DeleteDocumentAsync(int id);
        Task<IList<SearchProductResponse>> SearchProduct(string keyword);
    }
}
