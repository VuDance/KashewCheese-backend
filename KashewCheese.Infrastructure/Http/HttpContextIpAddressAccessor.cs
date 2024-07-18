using KashewCheese.Application.Common.Interfaces.Http;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Infrastructure.Http
{
    public class HttpContextIpAddressAccessor : IpAddressAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextIpAddressAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetIpAddress()
        {
            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
    }
}
