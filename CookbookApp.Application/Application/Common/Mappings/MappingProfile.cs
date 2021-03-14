using AutoMapper;
using CookbookApp.Application.Recipes.Queries.DTO;
using CookbookApp.Domain.Entities;

namespace CookbookApp.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<Recipe, RecipesChainDto>()
                .ForMember(r => r.Parent, o => o.Ignore());
        }
    }
}
