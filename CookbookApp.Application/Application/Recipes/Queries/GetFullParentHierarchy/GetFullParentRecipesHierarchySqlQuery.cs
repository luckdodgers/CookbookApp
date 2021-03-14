namespace CookbookApp.Application.Recipes.Queries.GetFullParentRecipesHierarchy
{
    public static class GetFullParentRecipesHierarchySqlQuery
    {
        public static string Generate(int childId) => _request.Replace("{0}", childId.ToString());

        private readonly static string _request = @"WITH RECURSIVE cte_recipes AS (
                    SELECT a.Id, a.Description, a.Created, a.ParentRecipeId
                    FROM Recipes a
                    WHERE a.Id = {0}

                    UNION ALL

                        SELECT r.Id, r.Description, r.Created, r.ParentRecipeId
                        FROM Recipes r
                        INNER JOIN cte_recipes t ON r.Id = t.ParentRecipeId
                        )

                    SELECT *
                    FROM cte_recipes
                    WHERE Id != {0}";
    }
}
