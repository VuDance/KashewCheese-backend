using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheAsync(string key, object value, TimeSpan expiration);
        Task<string> GetCacheAsync(string key);
        Task RemoveCacheAsync(string key);
    }

}
