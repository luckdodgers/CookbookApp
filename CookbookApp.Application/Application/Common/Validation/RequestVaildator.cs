//using CookbookApp.Application.Application.Common.Models;
//using CookbookApp.Application.Common.Models;
//using CookbookApp.Application.Domain.Entities;
//using CookbookApp.Infrastructure.Persistance;
//using FluentValidation;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CookbookApp.Application.Application.Common.Validation
//{
//    public class RequestVaildator
//    {
//        private readonly IEnumerable<IValidator> _validators;

//        public RequestVaildator(IEnumerable<IValidator> validators)
//        {
//            _validators = validators;
//        }

//        public bool IsValid<TResponse>(IRequest<TResponse> request) where TResponse : IRequestResult, new()
//        {
//            var validationFailures = _validators
//                .Where(validator => validator.CanValidateInstancesOfType(typeof(IRequest<TResponse>)))
//                .Select(validator => validator.Validate(new ValidationContext<IRequest<TResponse>>(request)))
//                .SelectMany(validationResult => validationResult.Errors)
//                .Where(validationFailure => validationFailure != null)
//                .ToList();

//            return !validationFailures.Any();

//            if (validationFailures.Any())
//            {
//                var error = string.Join("\r\n", validationFailures);
//                var response = new TResponse();
//                response.SetFail(Enums.RequestError.ValidationError, error);

//                return response;
//            }

//            private static async Task<int> AddAsync<TEntity>(TEntity entity)
//            where TEntity : class, IDomainEntity
//            {
//                var scope = ScopeFactory.CreateScope();
//                var context = scope.ServiceProvider.GetService<RecipeDbContext>();

//                context.Add(entity);

//                await context.SaveChangesAsync();

//                return entity.Id;
//            }

//            return await next();
//        }
//    }
//}
