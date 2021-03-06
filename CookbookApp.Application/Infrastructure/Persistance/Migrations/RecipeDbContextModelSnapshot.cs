// <auto-generated />
using System;
using CookbookApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CookbookApp.Migrations
{
    [DbContext(typeof(RecipeDbContext))]
    partial class RecipeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("CookbookApp.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime()");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentRecipeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentRecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CookbookApp.Domain.Entities.Recipe", b =>
                {
                    b.HasOne("CookbookApp.Domain.Entities.Recipe", "ParentRecipe")
                        .WithMany("ChildRecipes")
                        .HasForeignKey("ParentRecipeId");

                    b.Navigation("ParentRecipe");
                });

            modelBuilder.Entity("CookbookApp.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("ChildRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
