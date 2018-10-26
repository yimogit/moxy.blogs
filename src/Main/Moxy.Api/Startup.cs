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
using Moxy.Data;
using Moxy.Framework.Filters;
using Moxy.Framework.Middlewares;
using Moxy.Services.Article;
using Moxy.Services.System;
using Moxy.Swagger;
using Moxy.Swagger.Builder;
using Moxy.Swagger.Filters;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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

            //readme
            services.AddDbContextPool<MoxyDbContext>( // replace "YourDbContext" with the class name of your DbContext
               options => options.UseMySql(Configuration.GetConnectionString("Default"), // replace with your Connection String
                   mysqlOptions =>
                   {
                       mysqlOptions.ServerVersion(new Version(5, 7, 21), ServerType.MySql); // replace with your Server Version and Type
                   }
           ));
            //services
            //    .AddDbContext<MoxyDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("Default")));
            //.AddDbContext<MoxyDbContext>(opt => opt.UseInMemoryDatabase("MoxyDB"));
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IArticleService, ArticleService>();

            //版本控制
            services.AddMvcCore().AddJsonFormatters().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddCors();
            //忽略null
            services.AddMvc().AddJsonOptions(op =>
            {
                op.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                op.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            });
            //禁用默认的模型验证
            services.Configure<ApiBehaviorOptions>(opts => opts.SuppressModelStateInvalidFilter = true);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning(option =>
            {
                // allow a client to call you without specifying an api version
                // since we haven't configured it otherwise, the assumed api version will be 1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            });
            services.AddSwaggerCustom(CURRENT_SWAGGER_OPTIONS);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            app.UseMiddleware<ErrorHandlingMiddleware>();
            loggerFactory.AddLog4Net("log4net.config");

            var origins = Configuration.GetSection("CorsOrigins").GetChildren().Select(s => s.Value).ToArray();
            app.UseCors(policy => policy
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );
            app.UseMvc();
            //自动检测存在的版本
            CURRENT_SWAGGER_OPTIONS.ApiVersions = provider.ApiVersionDescriptions.Select(s => s.GroupName).ToList();
            //访问验证
            CURRENT_SWAGGER_OPTIONS.SwaggerAuthList = Configuration.GetSection("SwaggerCustomAuth")
                .GetChildren()
                .Select(s => s.Get<CustomSwaggerAuth>()).ToList();
            app.UseSwaggerCustom(CURRENT_SWAGGER_OPTIONS);

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
