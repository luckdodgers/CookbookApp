using CookbookApp.Application.Recipes.Commands.EditExistingRecipe;
using CookbookApp.Application.Recipes.Commands.EditRecipe;
using CookbookApp.Application.Recipes.Queries.DTO;
using CookbookApp.Application.Recipes.Queries.GetFullParentRecipesHierarchy;
using CookbookApp.Application.Recipes.Queries.GetNewestRecipes;
using CookbookApp.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookbookApp.Infrastructure.Controllers
{
    public class RecipesController : ApiController
    {
        public RecipesController(IErrorToStatusCodeConverter _errorToStatusCode) : base(_errorToStatusCode)
        {
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<RecipeDto>>> GetNewestRecipes()
        {
            var result = await Mediator.Send(new GetNewestRecipesQuery());

            if (result.Succeeded)
                return result.Value;

            return StatusCode(_errorToStatusCode.Convert(result.ErrorType));
        }

        [HttpGet("[action]/childid={childId}")]
        public async Task<ActionResult<RecipesChainDto>> GetWholeParentHierarchy(int childId)
        {
            var result = await Mediator.Send(new GetFullParentRecipesHierarchyQuery(childId));

            if (result.Succeeded)
                return result.Value;

            return StatusCode(_errorToStatusCode.Convert(result.ErrorType));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Unit>> AddNewRecipe(AddNewRecipeCommand command)
        {
            var result = await Mediator.Send(command);

            return result.Succeeded ?
                StatusCode(StatusCodes.Status201Created) : StatusCode(_errorToStatusCode.Convert(result.ErrorType));
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<Unit>> EditRecipe(EditExistingRecipeCommand command)
        {
            var result = await Mediator.Send(command);

            return result.Succeeded ?
                StatusCode(StatusCodes.Status204NoContent) : StatusCode(_errorToStatusCode.Convert(result.ErrorType));
        }
    }
}
