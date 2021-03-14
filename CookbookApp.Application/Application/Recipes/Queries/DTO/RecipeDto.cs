using System;

namespace CookbookApp.Application.Recipes.Queries.DTO
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}