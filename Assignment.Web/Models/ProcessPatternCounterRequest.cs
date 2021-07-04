using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Models
{
    public class ProcessPatternCounterRequest
    {
        public IFormFile File { get; set; }
    }
}
