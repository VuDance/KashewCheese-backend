using KashewCheese.API.Middlewares;

namespace KashewCheese.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IRouter UseRouterMiddleware(this IApplicationBuilder applicationBuilder)
        {
            var builder = new RouteBuilder(applicationBuilder);

            applicationBuilder.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Product/GetDetailProduct"), appBuilder =>
            {
                appBuilder.UseMiddleware<GetDetailProductMiddleware>();
            });
            applicationBuilder.UseWhen(context => context.Request.Path.StartsWithSegments("/api/User/GetUser"), appBuilder =>
            {
                appBuilder.UseMiddleware<UserMiddleware>();
            });


            return builder.Build();
        }
    }
}