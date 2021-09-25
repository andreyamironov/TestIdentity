using System;

namespace AMir.Exception
{
    public class PasswordConfirmException : System.Exception
    {
        public override string Message => "АМ Exception: Пароли не совпадают";
    }
}
