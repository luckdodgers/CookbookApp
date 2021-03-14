using AutoMapper;
using CookbookApp.Application.Common.Interfaces;
using CookbookApp.Application.Common.Models;
using CookbookApp.Application.Recipes.Queries.DTO;
using CookbookApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CookbookApp.Application.Recipes.Queries.GetNewestRecipes
{
    public class GetNewestRecipesQueryHandler : IRequestHandler<GetNewestRecipesQuery, QueryResult<List<RecipeDto>>>
    {
        private readonly IRecipeDbContext _context;
        private readonly IMapper _mapper;

        public GetNewestRecipesQueryHandler(IRecipeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<QueryResult<List<RecipeDto>>> Handle(GetNewestRecipesQuery request, CancellationToken cancellationToken)
        {
            var result = new List<RecipeDto>();

            try
            {
                var newestRecipes = await _context.Recipes
                .AsNoTracking()
                .Where(r => !r.ChildRecipes.Any())
                .ToListAsync();

                var sortedRecipes = newestRecipes.OrderBy(r => r.Description).ToList();

                result = _mapper.Map<IReadOnlyCollection<Recipe>, List<RecipeDto>>(sortedRecipes);
            }

            catch
            {
                return QueryResult<List<RecipeDto>>.InternalError();
            }

            return QueryResult<List<RecipeDto>>.Success(result);
        }
    }
}
