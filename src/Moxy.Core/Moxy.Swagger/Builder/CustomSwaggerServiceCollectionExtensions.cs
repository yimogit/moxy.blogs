using Microsoft.Extensions.DependencyInjection;
using Moxy.Swagger.Filters;
using Moxy.Swagger.Interface;
using Moxy.Swagger.Models;
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
                if (options.ControllerTags != null)
                {
                    c.DocumentFilter<TagDescriptionsDocumentFilter>();
                }
                options.AddSwaggerGenAction?.Invoke(c);

            });
            CustomSwaggerGlobalConfig.CURRENT_SWAGGER_TAGS = options.ControllerTags;
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
