using NUnit.Framework;
using System.Threading.Tasks;

namespace CookbookApp.Tests.Application.IntegrationTests
{
    using static ScopedRequest;
    class BaseTest
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ClearRecipes();
        }
    }
}
