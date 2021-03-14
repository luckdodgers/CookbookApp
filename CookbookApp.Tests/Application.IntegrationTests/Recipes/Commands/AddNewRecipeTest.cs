using CookbookApp.Application.Common.Enums;
using CookbookApp.Application.Recipes.Commands.EditRecipe;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CookbookApp.Tests.Application.IntegrationTests.Recipes.Commands
{
    using static ScopedRequest;

    class AddNewRecipeTest : BaseTest
    {
        [Test]
        public async Task SendInvalidCommand_ShouldReturnValidationError()
        {
            var command = new AddNewRecipeCommand()
            {
                ParentId = null,
                Description = string.Empty
            };

            var result = await SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.ErrorType.Should().Be(RequestError.ValidationError);
        }


        [Test]
        public async Task SendCommandWithNullParent_ShouldAddRecipeToDb()
        {
            var description = Arrange.GetRandomString();

            var command = new AddNewRecipeCommand()
            {
                ParentId = null,
                Description = description
            };

            var result = await SendAsync(command);
            var addedRecipe = await GetRecipeByDescriptionAsync(description);

            result.Succeeded.Should().BeTrue();
            addedRecipe.Should().NotBeNull();
            addedRecipe.ParentRecipe.Should().BeNull();
        }

        [Test]
        public async Task SendCommandWithParent_ShouldAddRecipeWithParentToDb()
        {
            // Arrange
            var description_child = Arrange.GetRandomString();
            var parentRecipe = await Arrange.SeedRandomRecipeAsync();
            var addChildCommand = new AddNewRecipeCommand()
            {
                ParentId = parentRecipe.Id,
                Description = description_child
            };

            // Act
            var childAddingResult = await SendAsync(addChildCommand);
            var childRecipe = await GetRecipeByDescriptionAsync(description_child);
            var parentRecipeFromDb = await GetRecipeAsync(parentRecipe.Id);

            // Assert
            childAddingResult.Succeeded.Should().BeTrue();
            parentRecipeFromDb.ChildRecipes.Should().Contain(cr => cr.Id == childRecipe.Id);
            childRecipe.ParentRecipe.Id.Should().Be(parentRecipe.Id);
        }
    }
}
