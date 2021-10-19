using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Modules.Pages.Commands.Create;
using Application.Modules.Pages.Models;
using Infrastructure.Persistence.Main;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Constants;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class PagesControllerTests : IntegrationTestBase, IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Startup> _appFactory;

        public PagesControllerTests(TestingWebAppFactory<Startup> appFactory) : base(appFactory)
        {
            _appFactory = appFactory;
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAll_WithoutAnyData_ReturnsEmptyList()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(ApiRoutes.Pages.GetAll);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<IList<PageDto>>();
            Assert.Empty(result);
        }

        [Fact]
        public async Task Create_WithPage_ReturnsCreated()
        {
            // Arrange
            var db = GetDbContext();

            var pageToCreate = new CreatePageCommand()
            {
                Name = "Main Page",
                Link = "/"
            };

            // Act
            var response = await _client.PostAsJsonAsync(ApiRoutes.Pages.Create, pageToCreate);

            // Assert
            var resultPage = await response.Content.ReadFromJsonAsync<PageDto>();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Assert.True(resultPage.Id > 0);
            Assert.Equal(pageToCreate.Name, resultPage.Name);
            Assert.Equal(pageToCreate.Link, resultPage.Link);
            Assert.True(db.Pages.Any());
            
            var pageInDb = db.Pages.First();

            Assert.Equal(resultPage.Id, pageInDb.Id);
            Assert.Equal(resultPage.Name, pageInDb.Name);
            Assert.Equal(resultPage.Link, pageInDb.Link);
        }
    }
}