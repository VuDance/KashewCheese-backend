using Amazon.Runtime;
using Amazon.S3;
using Application.Interfaces;
using KashewCheese.Application.Common.Interfaces.Authentication;
using KashewCheese.Application.Common.Interfaces.ElasticSearch;
using KashewCheese.Application.Common.Interfaces.File;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.Common.Services;
using KashewCheese.Infrastructure.Authentication;
using KashewCheese.Infrastructure.ElasticSearch;
using KashewCheese.Infrastructure.File;
using KashewCheese.Infrastructure.Persistence;
using KashewCheese.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Persistence.Context;
using Persistence.Redis;
using StackExchange.Redis;
using System.Text;


namespace KashewCheese.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddAuth(configuration)
                .AddContext(configuration)
                .AddPersistence()
                .AddCaching(configuration)
                .AddS3(configuration)
                .AddElasticsearch(configuration);
              
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }


        public static IServiceCollection AddPersistence(
            this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFileStorageService, S3FileStorageService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISkuRepository, SkuRepository>();
            /*services.AddScoped<IAdminstratorRepository, AdminstratorRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<ICustomerRepository, CustomersRepository>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped<IDispositionRepository, DispositionRepository>();*/

            return services;
        }
        public static IServiceCollection AddCaching(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration["RedisConfig:ConnectionStrings"]));
            services.AddStackExchangeRedisCache(opt => opt.Configuration = configuration["RedisConfig:ConnectionStrings"]);
            services.AddSingleton<ICacheService, RedisCacheService>();
            return services;
        }
        public static IServiceCollection AddElasticsearch(this IServiceCollection services, ConfigurationManager configuration)
        {
            var url = configuration["ElasticsearchSettings:Uri"];
            var defaultIndex = configuration["ElasticsearchSettings:DefaultIndex"];
            var username = configuration["ElasticsearchSettings:Username"];
            var password = configuration["ElasticsearchSettings:Password"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex).BasicAuthentication(username, password).ServerCertificateValidationCallback((o, certificate, chain, errors) => true);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            services.AddTransient(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>));

            return services;
        }

        public static IServiceCollection AddContext(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return services;
        }

        public static IServiceCollection AddS3(this IServiceCollection services,ConfigurationManager configuration)
        {
            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(
                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"]
            );
            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonS3>();
            return services;
        }

        public static IServiceCollection AddAuth(
        this IServiceCollection services, ConfigurationManager configuration)
        {
            var JwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, JwtSettings);

            services.AddSingleton(Options.Create(JwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(JwtSettings.Key))
                });
            return services;
        }
    }
}