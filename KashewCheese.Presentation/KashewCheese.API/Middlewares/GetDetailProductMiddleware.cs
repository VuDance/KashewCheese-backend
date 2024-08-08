using Application.Interfaces;
using AutoMapper;
using KashewCheese.Application.Constants;
using Newtonsoft.Json;

namespace KashewCheese.API.Middlewares
{
    public class GetDetailProductMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetDetailProductMiddleware(RequestDelegate next, ICacheService cacheService, IMapper mapper)
        {
            _next = next;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var routeData = context.GetRouteData();
            var idProduct=routeData.Values.TryGetValue("id", out var id);
            var key = _cacheService.GenerateCacheKey(KeyPrefix.Product+id,null, null);
            var cacheResponse = await _cacheService.GetCacheAsync(key);
            if (cacheResponse != null)
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cacheResponse);
                return;
            }
            else
            {
                await _next(context);
            }
        }
    }
}
