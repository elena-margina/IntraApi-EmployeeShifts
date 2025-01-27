using IntraApi.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace IntraApi.API.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TProgram>
            : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("DOTNET_ADDITIONAL_DEPS", Path.Combine(AppContext.BaseDirectory, "IntraApi.Api.deps.json"));

            builder.ConfigureServices(services =>
            {

                services.AddDbContext<IntraApiDBContext>(options =>
                {
                    options.UseInMemoryDatabase("IntraApiDBContextInMemoryTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<IntraApiDBContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                };
            });
        }

        public HttpClient GetAnonymousClient()
        {
            var client = CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost:7071")
            });

            return client;
        }
    }
}
