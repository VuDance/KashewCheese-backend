using KashewCheese.Contracts.ElasticSearch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Search.Queries.GetSearchResult
{
    public record GetSearchResultQuery : IRequest<string>;
}
