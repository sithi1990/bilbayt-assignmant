using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Infrastructure.Data.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        private readonly string _userName;

        public UserAlreadyExistsException(string userName)
        {
            _userName = userName;
        }
        public override string Message => $"The user {_userName} already exist.";
    }
}
