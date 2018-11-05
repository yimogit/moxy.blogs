using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moxy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Moxy.Framework
{
    public static class HttpRequestExtensions
    {
        const string AJAX_HEADER_KEY = "X-Requested-With";
        const string AJAX_HEADER_VALUE = "XMLHttpRequest";
        /// <summary>
        /// 根据Henader判断当前请求是否Ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjax(this HttpRequest httRequest)
        {
            return httRequest.Headers.Contains(new KeyValuePair<string, StringValues>(AJAX_HEADER_KEY, AJAX_HEADER_VALUE));
        }
        /// <summary>
        /// 请求的完整url
        /// </summary>
        /// <param name="httRequest"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpRequest httRequest)
        {
            return new StringBuilder()
                .Append(httRequest.Scheme)
                .Append("://")
                .Append(httRequest.Host)
                .Append(httRequest.PathBase)
                .Append(httRequest.Path)
                .Append(httRequest.QueryString)
                .ToString();
        }
        public static async Task EndWrite(this HttpContext httpContext, OperateResult resResult, HttpStatusCode code)
        {
            httpContext.Response.StatusCode = (int)code;
            if (httpContext.Request.IsAjax())
            {
                var result = JsonHelper.Serialize(resResult);
                httpContext.Response.ContentType = "application/json;charset=utf-8";

                await httpContext.Response.WriteAsync(result);
            }
            else
            {
                httpContext.Response.ContentType = "text/html;charset=utf-8";
                await httpContext.Response.WriteAsync(resResult.Msg);
            }
        }
    }
}
