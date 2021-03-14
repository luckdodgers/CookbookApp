using FluentValidation;

namespace CookbookApp.Application.Recipes.Queries.GetFullParentRecipesHierarchy
{
    public class GetFullParentRecipesHierarchyValidator : AbstractValidator<GetFullParentRecipesHierarchyQuery>
    {
        public GetFullParentRecipesHierarchyValidator()
        {
            RuleFor(c => c.ChildId).GreaterThan(0);
        }
    }
}
