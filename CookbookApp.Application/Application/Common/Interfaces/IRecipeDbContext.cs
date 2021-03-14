using CookbookApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CookbookApp.Application.Common.Interfaces
{
    public interface IRecipeDbContext
    {
        DbSet<Recipe> Recipes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
