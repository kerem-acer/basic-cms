using Application.Interfaces;
using Application.Interfaces.Persistence.Main;
using Infrastructure.Persistence.Main;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IMainAsyncRepository<>), typeof(MainAsyncRepository<>));

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}