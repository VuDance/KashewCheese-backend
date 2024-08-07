using KashewCheese.Contracts.Product;
using KashewCheese.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Products.Common
{
    public record GetProductsResult(List<GetProductsResponse> Products);
}
