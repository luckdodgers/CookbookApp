using CookbookApp.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CookbookApp.Tests.Domain.UnitTests.Entities
{
    class RecipeTests
    {
        private const string _descriprion = "test description";

        [Test]
        public void SetDescription_ShouldChangeDescriptionOnNew()
        {
            var recipe = new Recipe(_descriprion);
            var newDescription = "new description";

            recipe.SetDescription(newDescription);

            recipe.Description.Should().Be(newDescription);
        }

        [Test]
        public void AddChild_ShouldContainChildAfterAdding()
        {
            var recipe = new Recipe(_descriprion);
            var childRecipe = new Recipe(_descriprion);

            recipe.AddChild(childRecipe);

            recipe.ChildRecipes.Should().Contain(childRecipe);
            recipe.ChildRecipes.Should().HaveCount(1);
        }

        [Test]
        public void SetParent_ShouldSetNewParent()
        {
            var recipe = new Recipe(_descriprion);
            var parentRecipe = new Recipe(_descriprion);

            recipe.SetParent(parentRecipe);

            recipe.ParentRecipe.Should().Be(parentRecipe);
        }
    }
}
