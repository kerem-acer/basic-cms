using Application.Interfaces;
using Application.Interfaces.Cache;
using Application.Interfaces.Persistence.Main;
using Infrastructure.Cache.Redis;
using Infrastructure.Constants;
using Infrastructure.Persistence.Main;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration[PersistenceConfiguration.Main.ConnectionString] is not null)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration[PersistenceConfiguration.Main.ConnectionString]));
            }

            services.AddScoped(typeof(IMainAsyncRepository<>), typeof(MainAsyncRepository<>));

            services.AddTransient<IDateTime, DateTimeService>();

            var multiplexer = ConnectionMultiplexer.Connect(configuration[CacheConfigurations.ConnectionString]);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddTransient<ICacheManager, RedisCacheManager>();

            return services;
        }
    }
}