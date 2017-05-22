using System.Collections.Generic;

namespace Tasker.Helpers
{
    public static class AppVariables
    {
        private static readonly Dictionary<string, object> Values =
                   new Dictionary<string, object>();
        public static void SetValue(string key, object value)
        {
            if (Values.ContainsKey(key))
            {
                Values.Remove(key);
            }
            Values.Add(key, value);
        }
        public static T GetValue<T>(string key)
        {
            if (Values.ContainsKey(key))
            {
                return (T)Values[key];
            }
            return default(T);
        }
    }
}
