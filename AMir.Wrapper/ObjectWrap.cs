using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMir.Wrapper
{
    public static class ObjectWrap
    {
        public static object GetPropertyValue(this object entity, string propertyName,string defaultPropertyName)
        {
            try
            {
                return entity.GetType().GetProperties().Single(pi => string.Compare(pi.Name, propertyName, true) == 0).GetValue(entity, null);
            }
            catch
            {
                return entity.GetType().GetProperties().Single(pi => string.Compare(pi.Name, defaultPropertyName, true) == 0).GetValue(entity, null);
            }
        }
    }
}
