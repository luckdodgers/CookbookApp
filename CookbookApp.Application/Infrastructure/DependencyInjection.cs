using CookbookApp.Infrastructure.Interfaces;
using CookbookApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CookbookApp.Application.Infrastructure
{
    static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IErrorToStatusCodeConverter, RequestErrorToStatusCode>();

            return services;
        }
    }
}
