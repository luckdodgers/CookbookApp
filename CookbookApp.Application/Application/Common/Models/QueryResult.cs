using CookbookApp.Application.Common.Enums;
using System.Collections.Generic;

namespace CookbookApp.Application.Common.Models
{
    public class QueryResult<T> : RequestResult
    {
        /// <summary>
        /// Query result data
        /// </summary>
        public T Value { get; }

        private QueryResult(bool succeeded, RequestError errorType, IEnumerable<string> errors, T value = default) : base(succeeded, errorType, errors)
        {
            Value = value;
        }

        public QueryResult() { }

        public static QueryResult<T> Success(T resultValue) => new QueryResult<T>(true, RequestError.None, new string[0], resultValue);

        public static new QueryResult<T> Fail(RequestError errorType, IEnumerable<string> errors) => new QueryResult<T>(false, errorType, errors);

        public static new QueryResult<T> Fail(RequestError errorType, string error) => new QueryResult<T>(false, errorType, new string[] { error });

        public static new QueryResult<T> InternalError(string message = "Internal error") => Fail(RequestError.ApplicationException, message);
    }
}
