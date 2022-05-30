using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.Core
{
    public class OrderByHelper
    {
        public const string ASC = "asc";
        public const string DESC = "desc";
        public readonly string PropertyName;
        public readonly string CurrentAscDesc;

        public OrderByHelper(string propertyName, string defaultAscDesc)
        {
            if (propertyName != null)
            {
                if (propertyName.IndexOf("_desc") == -1)
                {
                    PropertyName = propertyName;
                    CurrentAscDesc = ASC;
                }
                else
                {
                    PropertyName = propertyName.Replace("_desc", "");
                    CurrentAscDesc = DESC;
                }
            }
            else
            {
                CurrentAscDesc = defaultAscDesc;
            }
        }
    }
}
