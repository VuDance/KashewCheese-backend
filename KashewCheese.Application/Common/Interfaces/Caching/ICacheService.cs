using KashewCheese.Application.DTO;

namespace Application.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheAsync(string key, object value, TimeSpan expiration);
        Task<string> GetCacheAsync(string key);
        Task RemoveCacheAsync(string key);

        string GenerateCacheKey(RequestInfoDto request, string? claims);
    }

}
