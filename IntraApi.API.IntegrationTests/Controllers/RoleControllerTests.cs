using IntraApi.API.IntegrationTests.Base;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text.Json;

namespace IntraApi.API.IntegrationTests.Controllers
{
    public class RoleControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public RoleControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        //[Fact]
        //public async Task ReturnsSuccessResult()
        //{
        //    var client = _factory.GetAnonymousClient();

        //    var response = await client.GetAsync("/api/Role/GetAllRoles");

        //    response.EnsureSuccessStatusCode();

        //    var responseString = await response.Content.ReadAsStringAsync();

        //    var result = JsonSerializer.Deserialize<List<RoleListVm>>(responseString);

        //    Assert.IsType<List<RoleListVm>>(result);
        //    Assert.NotEmpty(result);
        //}
    }
}
