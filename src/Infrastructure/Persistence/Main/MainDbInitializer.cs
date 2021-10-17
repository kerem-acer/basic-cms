using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Main
{
    public static class MainDbInitializer
    {
        public static async Task InitializeDbAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            await MigrateDb(context);
        }

        private static async Task MigrateDb(ApplicationDbContext context) 
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
    }
}