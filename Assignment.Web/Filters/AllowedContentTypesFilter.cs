using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Assignment.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Assignment.Web.Filters
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllowedContentTypesAttribute : Attribute, IFilterFactory
    {
        public AllowedContentTypesAttribute(string allowedContentTypes)
        {
            this.AllowedContentTypes = allowedContentTypes;
        }
        public string AllowedContentTypes { get; private set; }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetService<AllowedContentTypesFilter>();
            filter.AllowedContentTypes = AllowedContentTypes;
            return filter;
        }
    }

    public class AllowedContentTypesFilter : IResourceFilter
    {
        public string AllowedContentTypes { get; set; }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Ignore
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            List<ProblemDetails> errors = new List<ProblemDetails>();

            if(!context.HttpContext.Request.Form.Files.Any())
            {
                errors.Add(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Detail = "Please attach a file to process." });
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new JsonResult(new ResponseMetadata { Errors = errors });
                return;
            }

            var file = context.HttpContext.Request.Form.Files.FirstOrDefault();
            var allowedContentTypesList = AllowedContentTypes.Split(";");

            if(!allowedContentTypesList.Any(x => x == file.ContentType))
            {
                errors.Add(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Detail = "Unsupported file type." });
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new JsonResult(new ResponseMetadata { Errors = errors });
            }
        }
    }
}
