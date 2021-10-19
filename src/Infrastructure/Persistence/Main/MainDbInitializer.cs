using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Main
{
    public static class MainDbInitializer
    {
        public static async Task InitializeDbAsync(ApplicationDbContext db)
        {
            await MigrateDb(db);
        }

        private static async Task MigrateDb(ApplicationDbContext db) 
        {
            if (db.Database.IsSqlServer())
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}