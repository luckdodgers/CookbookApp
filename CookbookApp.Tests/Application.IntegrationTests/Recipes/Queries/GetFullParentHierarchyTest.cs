using CookbookApp.Application.Common.Enums;
using CookbookApp.Application.Recipes.Queries.GetFullParentRecipesHierarchy;
using CookbookApp.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookbookApp.Tests.Application.IntegrationTests.Recipes.Queries
{
    using static ScopedRequest;

    class GetFullParentHierarchyTest : BaseTest
    {
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public async Task SendInvalidQuery_ShouldReturnValidationError(int id)
        {
            var query = new GetFullParentRecipesHierarchyQuery(id);

            var queryResult = await SendAsync(query);

            queryResult.Succeeded.Should().BeFalse();
            queryResult.ErrorType.Should().Be(RequestError.ValidationError);
        }

        [Test]
        public async Task SendValidQuery_ShouldReturnHistoryHierarchy()
        {
            // Arrange
            var parentRecipe = await Arrange.SeedRandomRecipeAsync();

            Recipe curParentRecipe = parentRecipe;
            Recipe curChildRecipe = null;
            Stack<Recipe> hierarchyStack = new Stack<Recipe>(5);

            for (int i = 0; i < 5; i++)
            {
                curChildRecipe = await Arrange.SeedRandomRecipeAsChildAsync(curParentRecipe.Id);
                hierarchyStack.Push(curChildRecipe);
                curParentRecipe = curChildRecipe;
            }

            var query = new GetFullParentRecipesHierarchyQuery(hierarchyStack.Pop().Id);

            // Act
            var queryResult = await SendAsync(query);

            // Assert
            queryResult.Succeeded.Should().BeTrue();
            var curChild = queryResult.Value;

            while (curChild.Parent != null)
            {
                var stackedRecipe = hierarchyStack.Pop();
                curChild.Id.Should().Be(stackedRecipe.Id);
                curChild = curChild.Parent;
            }

            hierarchyStack.Should().BeEmpty();
        }
    }
}
