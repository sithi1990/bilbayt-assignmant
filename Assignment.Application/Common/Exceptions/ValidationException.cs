using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Assignment.Application.Common.Exceptions
{
    public class FailuresInfo
    {
        public FailuresInfo(string type, string message)
        {
            Type = type;
            Message = message;
        }
        public string Type { get; set; }
        public string Message { get; set; }
    }
    public class ValidationException : Exception
    {
        public IEnumerable<FailuresInfo> Failures { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            Failures = failures.Select(x => new FailuresInfo(x.PropertyName, x.ErrorMessage));
        }

       
    }
}
