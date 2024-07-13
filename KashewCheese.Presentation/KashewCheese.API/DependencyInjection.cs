using KashewCheese.API.Common;
using KashewCheese.API.Service;

namespace KashewCheese.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddMappings();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<JwtService>();
            return services;
        }
    }
}
