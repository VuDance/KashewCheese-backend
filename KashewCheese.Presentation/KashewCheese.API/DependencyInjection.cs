using KashewCheese.API.Common;
using KashewCheese.API.Extensions;
using KashewCheese.API.Mapping;
using KashewCheese.API.Service;
using System.Reflection;

namespace KashewCheese.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<JwtService>();
            services.AddAllProfiles(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
