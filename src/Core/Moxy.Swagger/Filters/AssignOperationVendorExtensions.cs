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
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "X-Requested-With",
                In = "header",
                Default = "XMLHttpRequest",
                Type = "string"
            });
        }
    }
}
