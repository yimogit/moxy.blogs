using Microsoft.Extensions.DependencyInjection;
using Moxy.Swagger.Filters;
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
                c.OperationFilter<SwaggerDefaultValueFilter>();
                options.AddSwaggerGenAction?.Invoke(c);

            });
            return services;
        }
        public static void ConfigureCustomSwagger(this IServiceCollection services, CustsomSwaggerOptions options)
        {
            //services.AddMvc(c =>
            //{
            //    c.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
            //});
            //services.AddMvcCore()
            //    .AddApiExplorer();
        }
    }
}
