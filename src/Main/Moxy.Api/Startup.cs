using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moxy.EntityFramework;
using Moxy.Services.Article;
using Moxy.Swagger;
using Moxy.Swagger.Builder;
using Moxy.Swagger.Filters;
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
                //c.Conventions.Add(new ApiExplorerGroupPerVersionConvention());

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //版本控制
            services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddApiVersioning(option =>
            {
                // allow a client to call you without specifying an api version
                // since we haven't configured it otherwise, the assumed api version will be 1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            });

            services
                //.AddDbContext<MoxyDbContext>(opt => opt.UseMySql("server=127.0.0.1;uid=root;pwd=123456;database=moxy_blogs_db_v2"))
                .AddDbContext<MoxyDbContext>(opt => opt.UseInMemoryDatabase("MoxyDB"));
            services.AddTransient<IArticleService, ArticleService>();

            services.AddSwaggerCustom(CURRENT_SWAGGER_OPTIONS);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //自动检测存在的版本
            CURRENT_SWAGGER_OPTIONS.ApiVersions = provider.ApiVersionDescriptions.Select(s => s.GroupName).ToArray();
            //访问验证
            CURRENT_SWAGGER_OPTIONS.SwaggerAuthList = Configuration.GetSection("SwaggerCustomAuth")
                .GetChildren()
                .Select(s => s.Get<CustomSwaggerAuth>()).ToList();
            app.UseSwaggerCustom(CURRENT_SWAGGER_OPTIONS);

            app.UseMvc();
        }

        /// <summary>
        /// 项目接口文档配置
        /// </summary>
        private CustsomSwaggerOptions CURRENT_SWAGGER_OPTIONS = new CustsomSwaggerOptions()
        {
            ProjectName = "墨玄涯博客接口",
            UseCustomIndex = true,
            RoutePrefix = "swagger",
            AddSwaggerGenAction = c =>
            {
                c.OperationFilter<AssignOperationVendorFilter>();
                var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml");
                //controller及action注释
                c.IncludeXmlComments(filePath, true);
            },
            UseSwaggerAction = c =>
            {

            },
            UseSwaggerUIAction = c =>
            {
            }
        };
    }
}
