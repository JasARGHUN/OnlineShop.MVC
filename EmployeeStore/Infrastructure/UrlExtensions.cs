using Microsoft.AspNetCore.Http;

namespace EmployeeStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request) => /*Метод PathAndQuery() работает с классом HttpRequest, для описания HTTP-запросов.
                                                                         Данный метод генерирует URL, по которому браузер будет возвращаться после обновления
                                                                         корзины принимая строку запроса*/
            request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();
    }
}
