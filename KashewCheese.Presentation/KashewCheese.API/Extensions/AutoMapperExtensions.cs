using AutoMapper;
using System.Reflection;

namespace KashewCheese.API.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AddAllProfiles(this IServiceCollection services, Assembly assembly)
        {
            var profiles = assembly.GetTypes()
                                   .Where(type => typeof(Profile).IsAssignableFrom(type) && !type.IsAbstract)
                                   .ToArray();

            services.AddAutoMapper(config =>
            {
                foreach (var profile in profiles)
                {
                    config.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            }, profiles.Select(p => p.Assembly).ToArray());
        }
    }
}
