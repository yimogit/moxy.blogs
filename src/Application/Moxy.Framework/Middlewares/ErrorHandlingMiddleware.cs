using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moxy.Utils;

namespace Moxy.Framework.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var loggerFactory = context.RequestServices.GetService<ILoggerFactory>();
            if (loggerFactory != null)
            {
                var logger = loggerFactory.CreateLogger("HandleException");
                logger.LogError(exception, exception.Message);
            }
           await context.EndWrite(OperateResult.Error("服务器错误，错误信息：" + exception.Message+",堆栈异常："+exception.StackTrace), HttpStatusCode.InternalServerError);
        }
    }
}
