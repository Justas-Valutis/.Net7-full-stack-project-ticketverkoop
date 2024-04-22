﻿using Newtonsoft.Json;

namespace TicketVerkoop.Extentions
{
    public static class SessionExtentions
    {
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
