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

namespace CookbookApp.Application.Recipes.Queries.GetFullParentRecipesHierarchy
{
    public class GetFullParentRecipesHierarchyQueryHandler : IRequestHandler<GetFullParentRecipesHierarchyQuery, QueryResult<RecipesChainDto>>
    {
        private readonly IRecipeDbContext _context;
        private readonly IMapper _mapper;

        public GetFullParentRecipesHierarchyQueryHandler(IMapper mapper, IRecipeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<QueryResult<RecipesChainDto>> Handle(GetFullParentRecipesHierarchyQuery request, CancellationToken cancellationToken)
        {
            RecipesChainDto result = null;

            try
            {
                var parents = await _context.Recipes
                   .FromSqlRaw(GetFullParentRecipesHierarchySqlQuery.Generate(request.ChildId))
                   .ToListAsync();

                if (parents.Count == 0)
                    return QueryResult<RecipesChainDto>.Fail(Common.Enums.RequestError.NotFound, $"No previous recipes for Id={request.ChildId}");

                result = MapToDtoHierarchy(parents);
            }

            catch
            {
                return QueryResult<RecipesChainDto>.InternalError();
            }

            return QueryResult<RecipesChainDto>.Success(result);
        }

        private RecipesChainDto MapToDtoHierarchy(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
                return null;

            var sortedRecipes = recipes.OrderByDescending(r => r.Id).ToList();
            var rootDto = _mapper.Map<RecipesChainDto>(sortedRecipes[0]);

            RecipesChainDto currentDto = rootDto;

            foreach (var recipe in sortedRecipes)
            {
                if (recipe == sortedRecipes[0])
                    continue;

                var newDto = _mapper.Map<RecipesChainDto>(recipe);
                currentDto.Parent = newDto;
                currentDto = newDto;
            }

            return rootDto;
        }
    }
}
