using Application.Interfaces;
using AutoMapper;
using KashewCheese.API.Attributes;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.Constants;
using KashewCheese.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace WebAPI
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public PermissionMiddleware(RequestDelegate next, ICacheService cacheService,IMapper mapper)
        {
            _next = next;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task InvokeAsync(HttpContext context, IRoleRepository roleRepository)
        {
            
            var endpoint = context.GetEndpoint();
            if (endpoint == null|| !RequiresAuthorization(endpoint))
            {
                await _next(context); // Bỏ qua middleware và chuyển tiếp yêu cầu
                return;
            }
            var data = RequiresAuthorization(endpoint);
            // Lấy user từ context
            var user = context.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("User is not authenticated");
                return;
            }


            var request = _mapper.Map<RequestInfoDto>(context.Request);
            var cacheKey =_cacheService.GenerateCacheKey(KeyPrefix.Permission,request.QueryParameters,user.Claims.Where(c => c.Type == "Email").Select(c => c.Value).First().ToString());
            var roles = context.User.Claims.FirstOrDefault(c => c.Type == "Roles")?.Value;
            var cacheResponse = await _cacheService.GetCacheAsync(cacheKey);

            List<string> permissions = new List<string>();


            if (!string.IsNullOrEmpty(cacheResponse))
            {
                string cacheResponseReplaced = cacheResponse.Replace("\\\"", "\"").Trim('"');
                permissions = JsonConvert.DeserializeObject<List<string>>(cacheResponseReplaced);
            }
            //ko thì gọi hàm check role và sau đó set role vào redis
            else
            {
                //get service của role
                var userRoles = await roleRepository.GetPermissionByEmail(user.Claims.Where(c => c.Type == "Email").First().Value);
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                List<string> stringRoles= new List<string>();
                foreach (var role in userRoles)
                {
                    stringRoles.Add(role.Name);
                }
                permissions = stringRoles;
                string jsonConvert = JsonConvert.SerializeObject(permissions,settings);
                await _cacheService.SetCacheAsync(cacheKey,jsonConvert,TimeSpan.FromHours(1));
            }
            // Kiểm tra quyền


            // Lấy route và method
            if (endpoint != null)
            {
                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                if (actionDescriptor != null)
                {
                    var requiredPermissions = actionDescriptor.MethodInfo
                        .GetCustomAttributes(typeof(AuthorizePermissionAttribute), false)
                        .Cast<AuthorizePermissionAttribute>()
                        .Select(a => a.Permission)
                        .ToList();

                    if (!requiredPermissions.All(rp => permissions.Contains(rp)))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("User does not have required permissions");
                        return;
                    }
                }
            }

            await _next(context);
        }
        private bool RequiresAuthorization(Endpoint endpoint)
        {
            // Kiểm tra xem endpoint có chứa attribute AuthorizePermissionAttribute hay không
            return endpoint.Metadata.Any(meta => meta.GetType() == typeof(AuthorizePermissionAttribute));
        }
        

    }

}
