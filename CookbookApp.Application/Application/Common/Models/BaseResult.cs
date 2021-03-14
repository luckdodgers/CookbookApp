using CookbookApp.Application.Application.Common.Models;
using CookbookApp.Application.Common.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CookbookApp.Application.Common.Models
{
    public abstract class BaseResult : IRequestResult
    {
        public virtual bool Succeeded { get; private set; }
        public RequestError ErrorType { get; private set; } = RequestError.None;
        public string[] Errors { get; protected set; }

        protected BaseResult() { }

        public void SetSuccess() => Succeeded = true;

        public void SetFail(RequestError errorType, IEnumerable<string> errors)
        {
            Succeeded = false;
            ErrorType = errorType;
            Errors = errors.ToArray();
        }

        public void SetFail(RequestError errorType, string error)
        {
            Succeeded = false;
            ErrorType = errorType;
            Errors = new string[] { error };
        }

        protected BaseResult(bool succeeded, RequestError errorType, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            ErrorType = errorType;
            Errors = errors.ToArray();
        }

        public string ErrorsToString()
        {
            if (Errors?.Length > 0)
                return string.Join('\n', Errors);

            return string.Empty;
        }
    }
}
