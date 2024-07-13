using KashewCheese.API.Service;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace KashewCheese.API.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddAutoMapper(typeof(AuthenticationMappingConfig).Assembly);
            return services;
        }
    }
}
