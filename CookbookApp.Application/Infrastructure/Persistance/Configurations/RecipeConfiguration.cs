using CookbookApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookbookApp.Infrastructure.Persistance.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Created)
                .HasDefaultValueSql("datetime()");

            builder.HasOne(child => child.ParentRecipe)
                .WithMany(parent => parent.ChildRecipes);

            builder.HasMany(parent => parent.ChildRecipes)
                .WithOne(child => child.ParentRecipe);
        }
    }
}
