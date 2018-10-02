using Moxy.Swagger.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Swagger.Filters
{
    /// <summary>
    /// 添加控制器描述标签
    /// </summary>
    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = CustomSwaggerGlobalConfig.CURRENT_SWAGGER_TAGS ?? new List<Tag>();
        }
    }
}
