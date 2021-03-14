using CookbookApp.Application.Recipes.Commands.EditRecipe;
using FluentValidation;

namespace CookbookApp.Application.Application.Recipes.Commands.AddNewRecipe
{
    public class AddNewRecipeValidator : AbstractValidator<AddNewRecipeCommand>
    {
        public AddNewRecipeValidator()
        {
            RuleFor(c => c.Description).NotEmpty();
        }
    }
}
