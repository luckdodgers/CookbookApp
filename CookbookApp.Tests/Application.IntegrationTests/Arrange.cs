using CookbookApp.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CookbookApp.Tests.Application.IntegrationTests
{
    using static ScopedRequest;

    public static class Arrange
    {
        /// <summary>
        /// Seed recipe
        /// </summary>
        /// <returns>Seeded recipe from database</returns>
        public static async Task<Recipe> SeedRandomRecipeAsync()
        {
            var description = GetRandomString();
            return await AddRecipe(new Recipe(description));
        }

        public static async Task<Recipe> SeedRandomRecipeAsChildAsync(int parentId)
        {
            var description = GetRandomString();
            return await AddRecipe(new Recipe(description), parentId);
        }

        public static string GetRandomString() => Guid.NewGuid().ToString();
    }
}
