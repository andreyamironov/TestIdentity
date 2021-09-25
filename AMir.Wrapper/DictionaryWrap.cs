using System;
using System.Collections.Generic;

namespace AMir.Wrapper
{
    public static class DictionaryWrap
    {
        public static string GetStringOrEmpty(this IDictionary<string, object> dictionary, string key,bool isRemove=false)
        {
            if (dictionary != null && dictionary.ContainsKey(key))
            {
                string val = dictionary[key].ToString();
                if (isRemove) dictionary.Remove(key);
                return val;
            }
            else return string.Empty;
        }
        public static void SetValue(this IDictionary<string, object> dictionary, string key, object value)
        {
            if (value != null && dictionary != null)
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
        }
    }
}
