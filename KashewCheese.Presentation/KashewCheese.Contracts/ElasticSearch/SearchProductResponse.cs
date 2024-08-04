using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Contracts.ElasticSearch
{
    public record SearchProductResponse(string Name,string Slug);
}
