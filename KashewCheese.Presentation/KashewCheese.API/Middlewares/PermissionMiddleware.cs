using Application.Interfaces;
using KashewCheese.API.Attributes;
using KashewCheese.Application.Common.Interfaces.Persistence;
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

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
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


            var cacheService = context.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = GenerateCacheKey(context.Request,user.Claims.Where(c => c.Type == "Email").Select(c => c.Value).First().ToString());
            var cacheResponse = await cacheService.GetCacheAsync(cacheKey);
            var roles = context.User.Claims.FirstOrDefault(c => c.Type == "Roles")?.Value;

            List<string> permissions = new List<string>();


            if (!string.IsNullOrEmpty(cacheResponse))
            {
                permissions = JsonConvert.DeserializeObject<List<string>>(cacheResponse);
            }
            //ko thì gọi hàm check role và sau đó set role vào redis
            else
            {
                //get service của role
                var userRoles = await roleRepository.GetRoleByEmail(user.Claims.Where(c => c.Type == "Email").First().Value);
                string jsonConvert = JsonConvert.SerializeObject(userRoles);
                await cacheService.SetCacheAsync(cacheKey,jsonConvert,TimeSpan.FromHours(1));
                List<string> stringRoles= new List<string>();
                foreach (var role in userRoles)
                {
                    stringRoles.Add(role.Name);
                }
                permissions = stringRoles;
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
        private static string GenerateCacheKey(HttpRequest request,string? claims)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            if(claims != null )
            {
                keyBuilder.Append($"|{claims}");
            }
            foreach(var (key, value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}---{value}");

            }
            
            return keyBuilder.ToString();

        }

    }

}
