using CookbookApp.Application.Domain.Entities;
using CookbookApp.Domain.Entities;
using CookbookApp.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CookbookApp.Tests.Application.IntegrationTests
{
    using static TestSetup;

    class ScopedRequest
    {
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var scope = ScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task<Recipe> AddRecipe(Recipe recipe, int? parentId = null)
        {
            var addedRecipeId = await AddAsync(recipe);

            if (parentId != null)
            {
                var scope = ScopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetService<RecipeDbContext>();

                var parentRecipe = await context.Recipes.FirstAsync(r => r.Id == parentId);
                parentRecipe.AddChild(recipe);

                await context.SaveChangesAsync();
            }

            return await GetRecipeAsync(addedRecipeId);
        }

        public static async Task<Recipe> GetRecipeAsync(int id)
        {
            var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<RecipeDbContext>();

            var recipe = await context.Recipes
                .AsNoTracking()
                .Include(r => r.ChildRecipes)
                .Include(r => r.ParentRecipe)
                .FirstOrDefaultAsync(r => r.Id == id);

            return recipe;
        }

        public static async Task<Recipe> GetRecipeByDescriptionAsync(string description)
        {
            var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<RecipeDbContext>();

            var recipe = await context.Recipes
                .AsNoTracking()
                .Include(r => r.ChildRecipes)
                .Include(r => r.ParentRecipe)
                .FirstOrDefaultAsync(r => r.Description == description);

            return recipe;
        }

        public static async Task ClearRecipes()
        {
            var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<RecipeDbContext>();

            context.Recipes.RemoveRange(context.Recipes);

            await context.SaveChangesAsync();
        }

        private static async Task<int> AddAsync<TEntity>(TEntity entity)
            where TEntity : class, IDomainEntity
        {
            var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<RecipeDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
