using Infrastructure.Persistence.Main;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;

namespace WebApi.IntegrationTests
{
    public class TestingWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("BasicCmsTestDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    
                    db.Database.EnsureCreated();

                    try
                    {
                        MainDbInitializer.InitializeDbAsync(db).Wait();
                    }
                    catch (Exception ex)
                    {
                        var logger = scopedServices
                            .GetRequiredService<ILogger<TestingWebAppFactory<TStartup>>>();

                        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                        throw;
                    }
                }
            });
        }
    }
}