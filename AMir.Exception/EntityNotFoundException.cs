using System;
using System.Collections.Generic;
using System.Text;

namespace AMir.Exception
{
    public class EntityNotFoundException : System.Exception
    {
        public override string Message => "АМ Exception: Сущность не найдена";
    }
}
