using CookbookApp.Application.Common.Models;
using CookbookApp.Application.Recipes.Queries.DTO;
using MediatR;

namespace CookbookApp.Application.Recipes.Queries.GetFullParentRecipesHierarchy
{
    public class GetFullParentRecipesHierarchyQuery : IRequest<QueryResult<RecipesChainDto>>
    {
        public int ChildId { get; }

        public GetFullParentRecipesHierarchyQuery(int childId)
        {
            ChildId = childId;
        }
    }
}
