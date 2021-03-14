using CookbookApp.Application.Common.Models;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CookbookApp.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : RequestResult, new()
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidationBehaviour(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
                .Where(validator => validator.CanValidateInstancesOfType(typeof(TRequest)))
                .Select(validator => validator.Validate(new ValidationContext<TRequest>(request)))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (validationFailures.Any())
            {
                var error = string.Join("\r\n", validationFailures);
                var response = new TResponse();
                response.SetFail(Enums.RequestError.ValidationError, error);

                return response;
            }

            return await next();
        }
    }
}
