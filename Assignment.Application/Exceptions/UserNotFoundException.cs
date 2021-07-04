using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public override string Message => "User not found";
    }
}
