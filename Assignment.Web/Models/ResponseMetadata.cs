using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Models
{
    public class ResponseMetadata
    {
        [JsonProperty("success")]
        public bool Success => Errors == null || !Errors.Any();

        [JsonProperty("errors")]
        public IEnumerable<ProblemDetails> Errors { get; set; }
    }
}
