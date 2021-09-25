using System;
using System.Collections.Generic;
using System.Text;

namespace AMir.Exception
{
    public class AdminRoleNotFoundException:System.Exception
    {
        public override string Message => "АМ Exception: Не найдена роль администратора";
    }
}
