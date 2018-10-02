using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moxy.Swagger;
using Moxy.Swagger.Builder;
using Moxy.Swagger.Filters;
using Moxy.Swagger.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace Moxy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(c =>
            {
                c.Conventions.Add(new ApiExplorerGroupPerVersionConvention());

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCustomSwagger(CURRENT_SWAGGER_OPTIONS);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCustomSwagger(CURRENT_SWAGGER_OPTIONS);
            app.UseStaticFiles();
            app.UseMvc();

        }

        /// <summary>
        /// 项目接口文档配置
        /// </summary>
        private CustsomSwaggerOptions CURRENT_SWAGGER_OPTIONS = new CustsomSwaggerOptions()
        {
            ProjectName = "墨玄涯博客接口",
            ApiVersions = new string[] { "v1", "v2" },
            UseCustomIndex = true,
            ControllerTags = new List<Tag>()
            {
                new Tag(){ Name="Test",Description="测试接口"}
            },
            UseSwaggerUIAction = c =>
            {

            },
            AddSwaggerGenAction = c =>
            {
                //还是应该放到外面去定义，不应该有耦合的
                c.OperationFilter<AssignOperationVendorFilter>();
                //typeof(CustsomSwaggerOptions).GetTypeInfo().Assembly.GetName()
                var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Moxy.Api.xml");
                c.IncludeXmlComments(filePath);
            }
        };
    }
}
