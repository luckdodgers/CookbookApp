using CookbookApp.Application.Recipes.Commands.EditExistingRecipe;
using FluentValidation;

namespace CookbookApp.Application.Application.Recipes.Commands.EditExistingRecipe
{
    public class EditExistingRecipeValidator : AbstractValidator<EditExistingRecipeCommand>
    {
        public EditExistingRecipeValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.Description).NotEmpty();
        }
    }
}
