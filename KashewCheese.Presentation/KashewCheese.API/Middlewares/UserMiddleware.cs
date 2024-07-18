using Application.Interfaces;
using AutoMapper;
using KashewCheese.Application.Constants;
using KashewCheese.Application.DTO;
using KashewCheese.Contracts.Roles;
using Newtonsoft.Json;

namespace KashewCheese.API.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public UserMiddleware(RequestDelegate next, ICacheService cacheService, IMapper mapper)
        {
            _next = next;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var request = _mapper.Map<RequestInfoDto>(context.Request);
            var key = _cacheService.GenerateCacheKey(KeyPrefix.User, request.QueryParameters, null);
            var cacheResponse = await _cacheService.GetCacheAsync(key);
            if (cacheResponse!=null)
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                string cacheResponseReplaced = _cacheService.ConvertData(cacheResponse);
                await context.Response.WriteAsync(cacheResponseReplaced);
                return;
            }
            else
            {
               await _next(context);
            }
        }
    }
}
