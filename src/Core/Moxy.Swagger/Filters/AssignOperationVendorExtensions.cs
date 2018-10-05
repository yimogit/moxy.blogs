using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moxy.Swagger.Filters
{
    /// <summary>
    /// 操作过滤器 可生成通用参数(query/form/header)
    /// </summary>
    public class AssignOperationVendorFilter : IOperationFilter
    {
        /// <summary>
        /// apply
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation == null || context == null)
                return;

            operation.Parameters = operation.Parameters ?? new List<IParameter>();

            //operation.Parameters.Insert(0, new NonBodyParameter() { Name = "X-Token", In = "header", Description = "身份验证票据", Required = false, Type = "string" });

        }
    }
}
