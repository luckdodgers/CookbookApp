using CookbookApp.Application.Common.Enums;
using CookbookApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CookbookApp.Infrastructure.Services
{
    public class RequestErrorToStatusCode : IErrorToStatusCodeConverter
    {
        private readonly ILogger<RequestErrorToStatusCode> _logger;

        public RequestErrorToStatusCode(ILogger<RequestErrorToStatusCode> logger)
        {
            _logger = logger;
        }

        public int Convert(RequestError error)
        {
            switch (error)
            {
                case RequestError.ApplicationException:
                    return StatusCodes.Status500InternalServerError;

                case RequestError.AlreadyExists:
                    return StatusCodes.Status409Conflict;

                case RequestError.NotFound:
                    return StatusCodes.Status404NotFound;

                case RequestError.ValidationError:
                case RequestError.OtherError:
                    return StatusCodes.Status400BadRequest;

                default:
                    _logger.LogWarning("No StatusCode for {errorType} found");
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
