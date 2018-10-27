using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moxy.Framework
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 模型验证自定义策略
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureModelStatePolicyCustom(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    if (!context.ModelState.IsValid)
                    {
                        var errorMsg = (from item in context.ModelState
                                        where item.Value.Errors.Any()
                                        select item.Value.Errors[0].ErrorMessage).FirstOrDefault();
                        return new JsonResult(OperateResult.Error(errorMsg));
                    }
                    return options.InvalidModelStateResponseFactory(context);
                };
            });
            return services;
        }
    }
}
