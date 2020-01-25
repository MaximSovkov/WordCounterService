using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.WebEncoders.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WordCounterService.Domain.Exceptions;
using WordCounterService.Web.ViewModels;

namespace WordCounterService.Web.Middlewares
{
    /// <summary>
    /// Exception handling middleware.
    /// </summary>
    public class DomainExceptionMiddleware
    {
        private static readonly IDictionary<Type, int> ExceptionStatusCodes = new Dictionary<Type, int>
        {
            [typeof(WordCounterServiceException)] = StatusCodes.Status400BadRequest
        };

        private readonly RequestDelegate next;
        private readonly IJsonHelper jsonHelper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next">Request delegate.</param>
        /// <param name="jsonHelper">JSON helper.</param>
        public DomainExceptionMiddleware(RequestDelegate next, IJsonHelper jsonHelper)
        {
            this.next = next;
            this.jsonHelper = jsonHelper;
        }

        /// <summary>
        /// Invokes the next middleware.
        /// </summary>
        /// <param name="httpContext">HTTP context.</param>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                var domainExceptionViewModel = CreateDomainExceptionViewModel(exception);

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = domainExceptionViewModel.StatusCode;
                httpContext.Response.ContentType = @"application/json";

                using var stringWriter = new StringWriter(new StringBuilder(200));
                jsonHelper.Serialize(domainExceptionViewModel).WriteTo(stringWriter, new HtmlTestEncoder());
                await httpContext.Response.WriteAsync(stringWriter.ToString());
            }
        }

        private DomainExceptionViewModel CreateDomainExceptionViewModel(Exception exception)
        {
            if (ExceptionStatusCodes.TryGetValue(exception.GetType(), out var statusCode))
            {
                return new DomainExceptionViewModel(statusCode, exception.Message);
            }

            return new DomainExceptionViewModel(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }
}
