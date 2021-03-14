using CookbookApp.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CookbookApp.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected readonly IErrorToStatusCodeConverter _errorToStatusCode;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ApiController(IErrorToStatusCodeConverter errorToStatusCode)
        {
            _errorToStatusCode = errorToStatusCode;
        }
    }
}