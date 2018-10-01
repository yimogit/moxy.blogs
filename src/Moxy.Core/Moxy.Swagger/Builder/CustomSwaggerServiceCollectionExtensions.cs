using Microsoft.Extensions.DependencyInjection;
using Moxy.Swagger.Filters;
using Moxy.Swagger.Interface;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Swagger.Builder
{
    public static class CustomSwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            return AddCustomSwagger(services, new CustsomSwaggerOptions());
        }
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, CustsomSwaggerOptions options)
        {
            services.AddSwaggerGen(c =>
            {
                if (options.ApiVersions == null) return;
                foreach (var version in options.ApiVersions)
                {
                    c.SwaggerDoc(version, new Info { Title = options.ProjectName, Version = version });
                }
                //还是应该放到外面去定义，不应该有耦合的
                c.OperationFilter<AssignOperationVendorFilter>();

                if (options.AddSwaggerGenAction == null) return;
                options.AddSwaggerGenAction(c);
            });
            ConfigureCustomSwagger(services, options);
            return services;
        }
        public static void ConfigureCustomSwagger(this IServiceCollection services, CustsomSwaggerOptions options)
        {
            services.AddMvc(c =>
            {
                c.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
            });
            services.AddMvcCore()
                .AddApiExplorer();
        }
    }
}
