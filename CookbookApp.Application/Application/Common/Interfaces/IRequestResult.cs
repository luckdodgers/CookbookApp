using CookbookApp.Application.Common.Enums;

namespace CookbookApp.Application.Application.Common.Models
{
    public interface IRequestResult
    {
        public bool Succeeded { get; }
        public RequestError ErrorType { get; }
        public string[] Errors { get; }

        abstract string ErrorsToString();
    }
}
