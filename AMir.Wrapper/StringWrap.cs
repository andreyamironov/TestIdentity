using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMir.Wrapper
{
    public static class StringWrap
    {
        public static IDictionary<string,string> SplitToKeyValue(this string text, char itemSeparator=';',char keyValueSeparator=':')
        {
            return text.Split(itemSeparator).Select(s => s.Split(keyValueSeparator)).ToDictionary(x=>x[0],x=>x[1]);
        }
    }
}
