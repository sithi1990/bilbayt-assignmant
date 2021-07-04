using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Application.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public override string Message => "Invalid password";
    }
}
