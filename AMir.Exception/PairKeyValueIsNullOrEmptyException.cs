using System;
using System.Collections.Generic;
using System.Text;

namespace AMir.Exception
{
    public class PairKeyValueIsNullOrEmptyException : System.Exception
    {
        public override string Message => "АМ Exception: для пары ключ/значение, недопустимы пустые значения";
    }
}
