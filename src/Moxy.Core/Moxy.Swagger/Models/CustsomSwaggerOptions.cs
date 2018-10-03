using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Swagger
{
    public class CustsomSwaggerOptions
    {
        public CustsomSwaggerOptions() { }
        public CustsomSwaggerOptions(string projectName, string[] apiVersions)
        {
            ProjectName = projectName;
            ApiVersions = apiVersions;
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = "My API";
        /// <summary>
        /// 接口文档显示版本
        /// </summary>
        public string[] ApiVersions { get; set; }
        /// <summary>
        /// 接口文档访问路由前缀
        /// </summary>
        public string RoutePrefix { get; set; } = "swagger";
        /// <summary>
        /// 使用自定义首页
        /// </summary>
        public bool UseCustomIndex { get; set; }
        /// <summary>
        /// UseSwagger Hook
        /// </summary>
        public Action<SwaggerOptions> UseSwaggerAction { get; set; }
        /// <summary>
        /// UseSwaggerUI Hook
        /// </summary>
        public Action<SwaggerUIOptions> UseSwaggerUIAction { get; set; }
        /// <summary>
        /// AddSwaggerGen Hook
        /// </summary>
        public Action<SwaggerGenOptions> AddSwaggerGenAction { get; set; }
    }
}
