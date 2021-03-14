using CookbookApp.Application.Common.Enums;
using CookbookApp.Application.Recipes.Commands.EditExistingRecipe;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CookbookApp.Tests.Application.IntegrationTests.Recipes.Commands
{
    using static ScopedRequest;

    class EditExistingRecipeTest : BaseTest
    {
        [Test]
        [TestCase(-1, "test_string")]
        [TestCase(1, "")]
        public async Task SendInvalidCommand_ShouldReturnValidationError(int id, string description)
        {
            var command = new EditExistingRecipeCommand()
            {
                Id = id,
                Description = string.Empty
            };

            var result = await SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.ErrorType.Should().Be(RequestError.ValidationError);
        }

        [Test]
        public async Task SendCommandWithValidCommand_ShouldEditRecipe()
        {
            var recipeToEdit = await Arrange.SeedRandomRecipeAsync();
            var newDescription = Arrange.GetRandomString();
            var editCommand = new EditExistingRecipeCommand()
            {
                Id = recipeToEdit.Id,
                Description = newDescription
            };

            var result = await SendAsync(editCommand);
            var editedRecipe = await GetRecipeAsync(recipeToEdit.Id);

            result.Succeeded.Should().BeTrue();
            editedRecipe.Should().NotBeNull();
        }
    }
}
