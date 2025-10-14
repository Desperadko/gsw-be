using GSW_Core.Responses;
using GSW_Core.Utilities.Errors.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Utilities.Errors
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> logger;
        private readonly IWebHostEnvironment environment;

        public ExceptionFilter(
            ILogger<ExceptionFilter> logger,
            IWebHostEnvironment environment)
        {
            this.logger = logger;
            this.environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, $"An error occured: {context.Exception.Message}");

            var errorResponse = new ErrorResponse
            {
                Error = context.Exception.Message
            };

            if (environment.IsDevelopment())
            {
                errorResponse.Details = [context.Exception.StackTrace]; 
            }

            context.Result = context.Exception switch
            {
                UnauthorizedException => new UnauthorizedObjectResult(errorResponse),
                NotFoundException => new NotFoundObjectResult(errorResponse),
                BadRequestException => new BadRequestObjectResult(errorResponse),
                _ => new ObjectResult(new ErrorResponse { Error = "An unexpected error occurred." })
                {
                    StatusCode = 500
                }
            };

            context.ExceptionHandled = true;
        }
    }
}
