using KashewCheese.Contracts.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Common.Interfaces.ElasticSearch
{
    public interface IElasticSearchService<T>
    {
        Task<string> CreateDocumentAsync(T document);
        Task<T> GetDocumentAsync(int id);
        Task<IEnumerable<T>> GetAllDocument();
        Task<string> UpdateDocumentAsync(T document);
        Task<string> DeleteDocumentAsync(int id);
    }
}
