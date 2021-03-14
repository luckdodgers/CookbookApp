using CookbookApp.Application.Common.Interfaces;
using CookbookApp.Application.Common.Models;
using CookbookApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CookbookApp.Application.Recipes.Commands.EditRecipe
{
    public class AddNewRecipeCommandHandler : IRequestHandler<AddNewRecipeCommand, RequestResult>
    {
        private readonly IRecipeDbContext _context;

        public AddNewRecipeCommandHandler(IRecipeDbContext context)
        {
            _context = context;
        }

        public async Task<RequestResult> Handle(AddNewRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newRecipe = new Recipe(request.Description);
                await _context.Recipes.AddAsync(newRecipe);

                if (request.ParentId != null)
                {
                    var parentRecipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == request.ParentId);

                    if (parentRecipe == null)
                        return RequestResult.Fail(Common.Enums.RequestError.NotFound, $"Requested ParentId={request.ParentId} not found");

                    parentRecipe.AddChild(newRecipe);
                }

                await _context.SaveChangesAsync();
            }

            catch
            {
                return RequestResult.InternalError();
            }

            return RequestResult.Success();
        }
    }
}
