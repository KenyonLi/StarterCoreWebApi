//*******************************
// Create By Ahoo Wang
// Date 2019-04-23 23:30
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Starter.Entity;
using Starter.WebApi.Exceptions;
using System.Net;

namespace Starter.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger<GlobalExceptionFilter> logger;

        public GlobalExceptionFilter(IHostingEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var exception = context.Exception;
            logger.LogError(
            new EventId(exception.HResult),
            exception,
            exception.Message);
            string errorCode = "500";
            if (exception is APIException) { errorCode = (exception as APIException).ErrorCode; }
            var errorResp = new ApiResult<string>
            {
                Message = exception.Message,
                StatusCode = errorCode,
                IsSuccess = false
            };
            var result = new JsonResult(errorResp)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            context.Result = result;

        }
    }
}






