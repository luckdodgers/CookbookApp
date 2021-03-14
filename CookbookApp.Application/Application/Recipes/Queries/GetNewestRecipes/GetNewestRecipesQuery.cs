using CookbookApp.Application.Common.Models;
using CookbookApp.Application.Recipes.Queries.DTO;
using MediatR;
using System.Collections.Generic;

namespace CookbookApp.Application.Recipes.Queries.GetNewestRecipes
{
    public class GetNewestRecipesQuery : IRequest<QueryResult<List<RecipeDto>>>
    {
    }
}
