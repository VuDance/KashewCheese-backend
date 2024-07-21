using KashewCheese.Application.Services.Cart.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Cart.Queries
{
    public class GetUserCartQueryHandler : IRequestHandler<GetUserCartQuery,GetUserCartResult>
    {
        Task<GetUserCartResult> IRequestHandler<GetUserCartQuery, GetUserCartResult>.Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
