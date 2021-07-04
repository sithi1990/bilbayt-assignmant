using Assignment.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Assignment.Application.Common.Exceptions;
using Newtonsoft.Json.Serialization;

namespace Assignment.Web.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var errorType = exceptionHandlerPathFeature.Error.GetType();

                    if(exceptionHandlerPathFeature.Error.GetType() == typeof(ValidationException))
                    {
                        var validationException = exceptionHandlerPathFeature.Error as ValidationException;

                        var failures = validationException.Failures.Select(x => new ProblemDetails
                        {
                            Detail = x.Message,
                            Type = x.Type,
                            Title = "Bad Request"
                        });
                        await SendErrorResponse(context, failures);
                        return;
                    }
                    else if (errorType.Namespace.StartsWith("Assignment.Application.Exceptions") || errorType.Namespace.StartsWith("Assignment.Infrastructure.Data.Exceptions"))
                    {
                        var pd = new ProblemDetails
                        {
                            Detail = exceptionHandlerPathFeature?.Error?.Message,
                            Title = "Bad Request"
                        };

                        await SendErrorResponse(context, new List<ProblemDetails> { pd });
                        return;
                    }
                    
                    else
                    {
                        var logger = context.RequestServices.GetRequiredService<ILogger>();
                        logger.LogError(exceptionHandlerPathFeature.Error.Message, exceptionHandlerPathFeature.Error);
                    }
                   
                    
                });
            });
        }

        private static async Task SendErrorResponse(HttpContext context, IEnumerable<ProblemDetails> errors)
        {
            var response = JsonConvert.SerializeObject(new ResponseMetadata { Errors = errors });
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response);
        }
    }
}
