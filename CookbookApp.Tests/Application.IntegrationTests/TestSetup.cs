using CookbookApp.Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.IO;

namespace CookbookApp.Tests.Application.IntegrationTests
{
    [SetUpFixture]
    class TestSetup
    {
        public static IServiceScopeFactory ScopeFactory { get; private set; }

        private static IConfigurationRoot _configuration;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Testing.json", true, true)
            .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "CookbookApp.Application"));

            startup.ConfigureServices(services);

            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            EnsureDatabase();
        }

        private static void EnsureDatabase()
        {
            using var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<RecipeDbContext>();
            context.Database.Migrate();
        }
    }
}
