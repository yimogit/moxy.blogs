using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moxy.Swagger;
using Moxy.Swagger.Builder;
using Moxy.Swagger.Filters;

namespace Moxy.EntityFramework.Tests
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // use in memory for testing.
            //services
            //    .AddDbContext<MoxyDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("Default")))
            //    //.AddDbContext<MoxyDbContext>(opt => opt.UseInMemoryDatabase("MoxyDB"))
            //    .AddUnitOfWork<MoxyDbContext>();

            //services.AddSwaggerCustom(CURRENT_SWAGGER_OPTIONS);
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_0); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            //app.UseSwaggerCustom(CURRENT_SWAGGER_OPTIONS);
            app.UseMvc();
        }
        /// <summary>
        /// 项目接口文档配置
        /// </summary>
        private CustsomSwaggerOptions CURRENT_SWAGGER_OPTIONS = new CustsomSwaggerOptions()
        {
            ProjectName = "墨玄涯博客接口",
            ApiVersions = new List<string>() { "v1" },
            UseCustomIndex = true,
            RoutePrefix = "swagger",
            SwaggerAuthList = new List<CustomSwaggerAuth>()
            {
                new CustomSwaggerAuth("yimo","123456")
            },
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
    public class ValidateModelStateAttribute : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errorList = new List<string>();
                foreach (var modelState in context.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errorList.Add(error.ErrorMessage);
                    }
                }
                context.Result = new JsonResult(new { success = false, message = errorList });
            }
        }
    }
}
