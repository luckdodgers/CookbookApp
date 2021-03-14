using CookbookApp.Application.Common.Behaviours;
using CookbookApp.Application.Common.Interfaces;
using CookbookApp.Infrastructure.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CookbookApp.Application
{
    static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var x = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<RecipeDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            Assembly.GetAssembly(typeof(Startup)).GetTypesAssignableFrom<IValidator>().ForEach((t) =>
            {
                services.AddScoped(typeof(IValidator), t);
            });

            services.AddScoped(typeof(IRecipeDbContext), typeof(RecipeDbContext));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }

        private static List<Type> GetTypesAssignableFrom<T>(this Assembly assembly)
        {
            return assembly.GetTypesAssignableFrom(typeof(T));
        }

        private static List<Type> GetTypesAssignableFrom(this Assembly assembly, Type compareType)
        {
            List<Type> result = new List<Type>();

            foreach (var type in assembly.DefinedTypes)
            {
                if (compareType.IsAssignableFrom(type) && compareType != type)
                {
                    result.Add(type);
                }
            }

            return result;
        }
    }
}
