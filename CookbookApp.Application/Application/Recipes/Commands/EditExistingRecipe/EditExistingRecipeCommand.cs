using CookbookApp.Application.Common.Models;
using MediatR;

namespace CookbookApp.Application.Recipes.Commands.EditExistingRecipe
{
    public class EditExistingRecipeCommand : IRequest<RequestResult>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
