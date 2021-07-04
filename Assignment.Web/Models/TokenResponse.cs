using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Models
{
    public class TokenResponse : ResponseMetadata
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
