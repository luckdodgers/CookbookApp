using CookbookApp.Application.Common.Interfaces;
using CookbookApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CookbookApp.Infrastructure.Persistance
{
    public class RecipeDbContext : DbContext, IRecipeDbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(Startup)));
            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferField);

            base.OnModelCreating(modelBuilder);
        }
    }
}
