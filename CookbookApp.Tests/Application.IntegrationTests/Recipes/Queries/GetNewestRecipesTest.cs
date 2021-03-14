using CookbookApp.Application.Recipes.Queries.GetNewestRecipes;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CookbookApp.Tests.Application.IntegrationTests.Recipes.Queries
{
    using static ScopedRequest;

    class GetNewestRecipesTest : BaseTest
    {
        [Test]
        public async Task SendRequestWhileNoRecipeExits_ShouldReturnEmptyCollection()
        {
            await ClearRecipes();

            var result = await SendAsync(new GetNewestRecipesQuery());

            result.Succeeded.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }

        [Test]
        public async Task SendRequestWhileDataExits_ShouldReturnAllNewestRecipes()
        {
            var parentRecipe = await Arrange.SeedRandomRecipeAsync();
            var childRecipe = await Arrange.SeedRandomRecipeAsChildAsync(parentRecipe.Id);
            var recipeWithNoHierarchy = await Arrange.SeedRandomRecipeAsync();

            var result = await SendAsync(new GetNewestRecipesQuery());

            result.Succeeded.Should().BeTrue();
            result.Value.Should().OnlyContain(r => r.Id == childRecipe.Id || r.Id == recipeWithNoHierarchy.Id);
        }
    }
}
