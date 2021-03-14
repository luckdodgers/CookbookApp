using CookbookApp.Application.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CookbookApp.Domain.Entities
{
    public class Recipe : IDomainEntity
    {
        public Recipe(string description)
        {
            Description = description;
        }

        private Recipe() { }

        public int Id { get; }
        public DateTime Created { get; }
        public string Description { get; private set; }
        public IReadOnlyCollection<Recipe> ChildRecipes => _childRecipes;
        public Recipe ParentRecipe { get; private set; }

        private List<Recipe> _childRecipes = new List<Recipe>();

        public void SetDescription(string description) => Description = description;
        public void AddChild(Recipe newRecipe) => _childRecipes.Add(newRecipe);
        public void SetParent(Recipe parent) => ParentRecipe = parent;
    }
}
