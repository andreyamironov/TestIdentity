using System;
using System.Collections.Generic;
using System.Text;

namespace AMir.Exception
{
    public class EntityDuplicateException : System.Exception
    {
        public override string Message => "АМ Exception: Дублирование сущности в контексте";
    }
}
