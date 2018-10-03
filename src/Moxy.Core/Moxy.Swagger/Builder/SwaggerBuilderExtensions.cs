using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;
using Microsoft.AspNetCore.Http;
using Moxy.Swagger.Utils;

namespace Moxy.Swagger.Builder
{
    public static class SwaggerBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            return UseCustomSwagger(app, new CustsomSwaggerOptions());
        }
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, CustsomSwaggerOptions options)
        {
            app
            .UseCustomSwaggerAuth(options)
            .UseSwagger(opt =>
            {
                if (options.UseSwaggerAction == null) return;
                options.UseSwaggerAction(opt);
            })
            .UseSwaggerUI(c =>
            {
                if (options.ApiVersions == null) return;
                c.RoutePrefix = options.RoutePrefix;
                c.DocumentTitle = options.ProjectName;
                if (options.UseCustomIndex)
                {
                    c.UseCustomSwaggerIndex();
                }
                if (options.SwaggerAuthList.Count > 0)
                {
                    c.ConfigObject["customAuth"] = true;
                    c.ConfigObject["loginUrl"] = $"/{options.RoutePrefix}/login.html";
                    c.ConfigObject["logoutUrl"] = $"/{options.RoutePrefix}/logout";
                }
                foreach (var item in options.ApiVersions)
                {
                    c.SwaggerEndpoint($"/swagger/{item}/swagger.json", $"{item}");
                }
                options.UseSwaggerUIAction?.Invoke(c);
            });
            return app;
        }
        private const string SWAGGER_ATUH_COOKIE = nameof(SWAGGER_ATUH_COOKIE);
        private static IApplicationBuilder UseCustomSwaggerAuth(this IApplicationBuilder app, CustsomSwaggerOptions options)
        {
            if (options.SwaggerAuthList.Count == 0)
                return app;
            var currentAssembly = typeof(CustsomSwaggerOptions).GetTypeInfo().Assembly;
            app.Use(async (context, next) =>
            {
                var _method = context.Request.Method.ToLower();
                var _path = context.Request.Path.Value;
                if (_path.IndexOf($"/{options.RoutePrefix}") != 0)
                {
                    await next();
                    return;
                }
                else if (_path == $"/{options.RoutePrefix}/login.html")
                {
                    //登录
                    if (_method == "get")
                    {
                        var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.login.html");
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        context.Response.ContentType = "text/html;charset=utf-8";
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        context.Response.Body.Write(buffer, 0, buffer.Length);
                        return;
                    }
                    else if (_method == "post")
                    {
                        var userModel = new CustomSwaggerAuth(context.Request.Form["userName"], context.Request.Form["userPwd"]);
                        if (!options.SwaggerAuthList.Any(e => e.UserName == userModel.UserName && e.UserPwd == userModel.UserPwd))
                        {
                            await context.Response.WriteAsync("login error!");
                            return;
                        }
                        //context.Response.Cookies.Append("swagger_auth_name", userModel.UserName);
                        context.Response.Cookies.Append(SWAGGER_ATUH_COOKIE, userModel.AuthStr);
                        context.Response.Redirect($"/{options.RoutePrefix}");
                        return;
                    }
                }
                else if (_path == $"/{options.RoutePrefix}/logout")
                {
                    //退出
                    context.Response.Cookies.Delete(SWAGGER_ATUH_COOKIE);
                    context.Response.Redirect($"/{options.RoutePrefix}/login.html");
                    return;
                }
                else
                {
                    if (!options.SwaggerAuthList.Any(s => !string.IsNullOrEmpty(s.AuthStr) && s.AuthStr == context.Request.Cookies[SWAGGER_ATUH_COOKIE]))
                    {
                        context.Response.Redirect($"/{options.RoutePrefix}/login.html");
                        return;
                    }
                }
                await next();
            });
            return app;
        }
        /// <summary>
        /// 使用自定义首页
        /// </summary>
        /// <returns></returns>
        public static void UseCustomSwaggerIndex(this SwaggerUIOptions c)
        {
            var currentAssembly = typeof(CustsomSwaggerOptions).GetTypeInfo().Assembly;
            c.IndexStream = () => currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.index.html");
        }
        /// <summary>
        /// 使用登录页
        /// </summary>
        /// <returns></returns>
        public static void WriteSwaggerPage(this HttpResponse response, string page)
        {
            var currentAssembly = typeof(CustsomSwaggerOptions).GetTypeInfo().Assembly;
            var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.{page}.html");
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            response.ContentType = "text/html;charset=utf-8";
            response.StatusCode = StatusCodes.Status200OK;
            response.Body.Write(buffer, 0, buffer.Length);
        }
    }
}
