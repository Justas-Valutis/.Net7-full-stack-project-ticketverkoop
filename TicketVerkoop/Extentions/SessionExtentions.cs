using Newtonsoft.Json;

namespace TicketVerkoop.Extentions
{
    public static class SessionExtentions
    {
        // Extension methods, as the name suggests, are additional methods.
        // Extension methods allow you to inject additional methods
        // without modifying, deriving or 

        public static void SetObject(this ISession session, string key, object? value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key); //JSON-Object
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
