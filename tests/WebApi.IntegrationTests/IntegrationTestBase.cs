using System;
using System.Net.Http;
using Infrastructure.Persistence.Main;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace WebApi.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        private readonly TestingWebAppFactory<Startup> _appFactory;

        protected IntegrationTestBase(TestingWebAppFactory<Startup> appFactory)
        {
            _appFactory = appFactory;
        }

        protected ApplicationDbContext GetDbContext()
        {
            var scope = _appFactory.Services
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            return scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

    }
}
