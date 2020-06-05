using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EmployeeStore.Infrastructure
{
    public static class SessionExtensions
    {
        //Сериализуем и десериализуем данные.
        public static void SetJson(this ISession session, string key, object value) //Сохраняем данные в сессию.
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key) //Извлекаем данные из сессии.
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
