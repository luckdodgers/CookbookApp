using CookbookApp.Application.Common.Models;
using MediatR;

namespace CookbookApp.Application.Recipes.Commands.EditRecipe
{
    public class AddNewRecipeCommand : IRequest<RequestResult>
    {
        public int? ParentId { get; set; }
        public string Description { get; set; }
    }
}
