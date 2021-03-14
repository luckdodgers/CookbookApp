using CookbookApp.Application.Common.Enums;

namespace CookbookApp.Infrastructure.Interfaces
{
    public interface IErrorToStatusCodeConverter
    {
        int Convert(RequestError error);
    }
}