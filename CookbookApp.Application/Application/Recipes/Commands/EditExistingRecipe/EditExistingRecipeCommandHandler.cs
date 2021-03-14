using CookbookApp.Application.Common.Interfaces;
using CookbookApp.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CookbookApp.Application.Recipes.Commands.EditExistingRecipe
{
    public class EditExistingRecipeCommandHandler : IRequestHandler<EditExistingRecipeCommand, RequestResult>
    {
        private readonly IRecipeDbContext _context;

        public EditExistingRecipeCommandHandler(IRecipeDbContext context)
        {
            _context = context;
        }

        public async Task<RequestResult> Handle(EditExistingRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var recipeToEdit = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == request.Id);

                if (recipeToEdit == null)
                    return RequestResult.Fail(Common.Enums.RequestError.NotFound, $"Recipe Id={request.Id} not found");

                recipeToEdit.SetDescription(request.Description);

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
