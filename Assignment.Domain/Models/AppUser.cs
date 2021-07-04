using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Domain.Models
{
    public class AppUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHashed { get; set; }
        public string FullName { get; set; }
    }
}
