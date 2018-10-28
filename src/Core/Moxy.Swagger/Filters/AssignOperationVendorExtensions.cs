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
        private List<IParameter> defParameters = new List<IParameter>()
        {
            new NonBodyParameter()
            {
                Name = "Authorization",
                In = "header",
                Default = "",
                Type = "string"
            }
            ,new NonBodyParameter()
            {
                Name = "X-Requested-With",
                In = "header",
                Default = "XMLHttpRequest",
                Type = "string"
            }
        };
        public AssignOperationVendorFilter() { }
        public AssignOperationVendorFilter(List<IParameter> parameters)
        {
            defParameters.AddRange(parameters);
        }
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
            foreach (var item in defParameters)
            {
                operation.Parameters.Add(item);
            }
        }
    }
}
