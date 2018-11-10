using Microsoft.AspNetCore.Mvc.Filters;
using Moxy.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Moxy.Framework.Authentication;
using System.Net;
using Moxy.Utils;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Moxy.Services.System;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Moxy.Framework.Permissions
{
    public class AdminAuthAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 是否需要验证
            if (context.Filters.Any(e => (e as AllowAnonymousFilter) != null))
                return base.OnActionExecutionAsync(context, next);
            // 判断token的有效性
            var authResult = context.HttpContext.RequestServices.GetService<IMoxyAuth>().CheckSign();
            if (authResult.Status == ResultStatus.Error)
            {
                // token不正确或过期
                return context.HttpContext.EndWrite(authResult, HttpStatusCode.Unauthorized);
            }

            //获取接口的权限编码列表
            var permissionCodes = context.Filters
                .Where(e => e is PermissionAttribute)
                .Select(e => e as PermissionAttribute)
                .Select(s => s.ModuleCode)
                .ToList();
            if (permissionCodes.Count == 0)
            {
                //无权限标识，只要登录即可访问
                return base.OnActionExecutionAsync(context, next);
            }
            // 获取用户权限编码集合
            var authModel = authResult.GetData<MoxySignInModel>();
            // 获取当前用户权限编码
            var modulesResult = context.HttpContext.RequestServices.GetService<ISystemService>().GetAuthModuleCodes(authModel.AuthName, authModel.AuthKey);
            if (modulesResult.Status == ResultStatus.Error)
            {
                // 获取编码失败
                return context.HttpContext.EndWrite(modulesResult, HttpStatusCode.Unauthorized);
            }
            // * 为包含所有权限
            else if (modulesResult.GetData<string>() == "*")
            {
                return base.OnActionExecutionAsync(context, next);
            }
            else if (!modulesResult.GetData<string>().Split(',').ToList().Any(s => permissionCodes.Contains(s)))
            {
                //用户权限未包含特性标记中的code则无权限
                return context.HttpContext.EndWrite(OperateResult.Error("无权限访问"), HttpStatusCode.Unauthorized);
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
